using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FirstPersonController : MonoBehaviour
    {
        [SerializeField] private bool m_IsWalking;
        [SerializeField] private float m_WalkSpeed;
        [SerializeField] private float m_RunSpeed;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private MouseLook m_MouseLook;
        [SerializeField] private bool m_UseFovKick;
        [SerializeField] private FOVKick m_FovKick = new FOVKick();
        [SerializeField] private bool m_UseHeadBob;
        [SerializeField] private CurveControlledBob m_HeadBob = new CurveControlledBob();
        [SerializeField] private LerpControlledBob m_JumpBob = new LerpControlledBob();
        [SerializeField] private float m_StepInterval;
        [SerializeField] private AudioClip m_FootstepSounds;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip m_JumpSound;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip m_LandSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;

		float currentSpeed;

		public float CurrentSpeed{
			get{
				return currentSpeed;
			}
			set{
				currentSpeed = value;
			}
		}


		[SerializeField] public float sneakSpeed;
		[SerializeField] public float crouchSpeed;
		[SerializeField] public float minimumSpeed;
		private bool sneaking = false;
		private bool running = false;
		private bool crouching = false;
		private bool actuallyCrouched = false;
		[HideInInspector] public bool carriing = false;

		[SerializeField] public Transform head;

		public Transform RPcam;
		public Transform LPcam;
		public Transform basicCam;
		public static bool canPeekR = false;
		public static bool canPeekL = false;

		private float minSurviveFall; //ennyi idő alatt a levegőben nem szerez sérülést a játékos
		private float damageForSeconds; //sérülés mértéke 1 mp-enként a levegőben
		private float airTime;


        private PlayerHealth playerHealth;
        public Transform deadCam;


        // Use this for initialization
        private void Start()
        {
            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            m_HeadBob.Setup(m_Camera, m_StepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();
			m_MouseLook.Init(transform , m_Camera.transform);

			minSurviveFall = 1f;
			damageForSeconds = 1.5f;
			airTime = 0f;
			
            
            
            playerHealth.enabled = true;
            

        }

        private void Awake()
        {
            playerHealth = GetComponent<PlayerHealth>();
            playerHealth.enabled = true;

            //Debug.Log("firstperson awake");
        }


        private void Update()
		{
            if (!PlayerHealth.isDead)
            {
                m_Camera.transform.position = basicCam.position;
                head.position = basicCam.position;
            }
            else
            {
                m_Camera.transform.position = deadCam.position;
                m_Camera.transform.rotation = deadCam.rotation;
            }

        RotateView ();
			////////////////////////////////////////////////
			// the jump state needs to read here to make sure it is not missed
			if (!m_Jump) {
				//m_Jump = CrossPlatformInputManager.GetButtonDown ("Jump");
				if (!carriing) 
				{
					m_Jump = Input.GetKeyDown (GameManager.GM.jump);
				}
            }

			if (!m_PreviouslyGrounded && m_CharacterController.isGrounded) {
				StartCoroutine (m_JumpBob.DoBobCycle ());
				PlayLandingSound ();
				m_MoveDir.y = 0f;
				m_Jumping = false;
				if(airTime > minSurviveFall)
				{
					playerHealth.TakeDamage (damageForSeconds * airTime * 10);
				}
				airTime = 0;
			}
			if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded) {
				m_MoveDir.y = 0f;
			}
			if (!m_CharacterController.isGrounded && !m_Jump) {
				airTime += Time.deltaTime;
			}
			//////////////////////////////////////

			m_PreviouslyGrounded = m_CharacterController.isGrounded;
 

            if (Input.GetKey(GameManager.GM.sneak))
				sneaking = true;
			else
				sneaking = false;

			if (Input.GetKeyDown(GameManager.GM.crouch) && m_CharacterController.isGrounded && !carriing) {
				crouching = !crouching;
				actuallyCrouched = true;
			}
			if (Input.GetKey(GameManager.GM.run) && !carriing) 
			{
				running = true;
				sneaking = false;
			} else
				running = false;

			if (Input.GetKey(GameManager.GM.rightPeek)) {
				if (canPeekR == false) {
					if (running == false && m_Jumping == false) {
						m_Camera.transform.position = RPcam.position;
						m_Camera.transform.rotation = RPcam.rotation;
						head.position = RPcam.position;
					}
				}
			}

			if (Input.GetKey(GameManager.GM.leftPeek)) {
				if (canPeekL == false) {
					if (running == false && m_Jumping == false) {
						m_Camera.transform.position = LPcam.position;
						m_Camera.transform.rotation = LPcam.rotation;
						head.position = LPcam.position;
					}
				}
			}

			currentSpeed = m_WalkSpeed;

            if (crouching)
            {
                currentSpeed += crouchSpeed;
                m_CharacterController.height = 0.6f;
            }
            else
            {
                m_CharacterController.height = 1.8f;
            }
			if (sneaking)
				currentSpeed += sneakSpeed;
			if (running)
				currentSpeed += m_RunSpeed;
			if (currentSpeed < minimumSpeed)
				currentSpeed = minimumSpeed;


            if (Lockpick.lockPickInProgress == true || PlayerHealth.isDead)
            {
                currentSpeed = 0;
                MouseLook.XSensitivity = 0;
                MouseLook.YSensitivity = 0;
            }
            else
            {
                MouseLook.XSensitivity = MouseLook.XsenValue;
                MouseLook.YSensitivity = MouseLook.YsenValue;
            }
        }
        

        private void PlayLandingSound()
        {
			if (actuallyCrouched)
            {
                m_AudioSource.clip = null;
				actuallyCrouched = false;
            }
            else
            {
                m_AudioSource.clip = m_LandSound;
                m_AudioSource.Play();
                m_NextStep = m_StepCycle + .5f;
				HangAmiAkkorJonLetreHaRalepunkVagyUgrunkValamire (7f);
            }
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward * m_Input.y + transform.right * m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x * speed;
            m_MoveDir.z = desiredMove.z * speed;




            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump)
                {
                    if (crouching)
                    {
                        crouching = false;
                        m_Jump = false;
                    }
                    else
                    {
                        m_MoveDir.y = m_JumpSpeed;
                        PlayJumpSound();
                        m_Jump = false;
                        m_Jumping = true;
                    }
                }
            }
            else
            {
                m_MoveDir += Physics.gravity * m_GravityMultiplier * Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir * Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);

            m_MouseLook.UpdateCursorLock();
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = m_JumpSound;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(m_IsWalking ? 1f : m_RunstepLenghten)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + m_StepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            //int n = Random.Range(1, m_FootstepSounds.Length);
            m_AudioSource.clip = m_FootstepSounds;
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
			// hang kiadás !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			HangAmiAkkorJonLetreHaRalepunkVagyUgrunkValamire(CurrentSpeed);
            // move picked sound to index 0 so it's not picked next time
            //m_FootstepSounds[n] = m_FootstepSounds[0];
            //m_FootstepSounds[0] = m_AudioSource.clip;
        }

		private void HangAmiAkkorJonLetreHaRalepunkVagyUgrunkValamire(float vehemencia)
		{
			RaycastHit hit;
			Physics.Raycast (transform.position, Vector3.down, out hit);
            GameObject go = hit.collider.gameObject;
            while (go.GetComponent<ThiefObject>() == null && go.transform.parent != null)
            {
                go = go.transform.parent.gameObject;
            }
            if (go.GetComponent<ThiefObject>() == null)
            {
                Debug.Log("This shit that you stand on is not ThiefObject!");
                return;
            }
                
			ThiefObject thiefObj = go.GetComponent<ThiefObject>();
			float noise = thiefObj.material.NoiseGeneration (vehemencia);//a vehemecia az az érték, hogy milyen erősen léptünk a felületre

			Collider[] colliders = Physics.OverlapSphere(transform.position, noise);
            //Debug.Log(noise);
            foreach (Collider nearbyObjects in colliders)
			{
				Creature g = nearbyObjects.GetComponent<Creature>();
				float dist = Vector3.Distance(transform.position, nearbyObjects.transform.position);
				
				if(g != null)
				{
					if(dist < noise)
					{
						g.GetNoise((int)(((noise-dist)*100/noise)*1), this.gameObject.transform.position); //1 a gyanú pontok szorzója
						//Debug.Log(noise);
					}
				}
			}

			m_LandSound = thiefObj.material.MaterialSound();
            m_FootstepSounds = thiefObj.material.MaterialSound();
            //Még balancolandó, 0.035f egy noiseegység mekkora volumenak felel meg, 0.2f alaphangerő
            m_AudioSource.volume = noise * 0.035f + 0.2f;
		}

        private void UpdateCameraPosition(float speed)
        {
                Vector3 newCameraPosition;
                if (!m_UseHeadBob)
                {
                    return;
                }
                if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
                {
                    m_Camera.transform.localPosition =
                        m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                          (speed * (m_IsWalking ? 1f : m_RunstepLenghten)));
                    newCameraPosition = m_Camera.transform.localPosition;
                    newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
                }
                else
                {
                    newCameraPosition = m_Camera.transform.localPosition;
                    newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
                }
                m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

            bool waswalking = m_IsWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            m_IsWalking = !Input.GetKey(GameManager.GM.run);
#endif
            // set the desired speed to be walking or running
            //speed = m_IsWalking ? m_WalkSpeed : m_RunSpeed;
			speed = currentSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (m_IsWalking != waswalking && m_UseFovKick && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!m_IsWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }


        private void RotateView()
        {
            m_MouseLook.LookRotation (transform, m_Camera.transform);
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
