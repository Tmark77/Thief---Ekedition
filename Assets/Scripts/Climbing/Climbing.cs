using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Climbing : MonoBehaviour {

	private bool canClimb;
	private Rigidbody rb;
	private RigidbodyFirstPersonController cc;
	public Animator anim;
	public Camera climbCam;
	public Camera mainCam;

	public static bool alatetVisible;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		cc = GetComponent<RigidbodyFirstPersonController> ();
	}
	
	// Update is called once per frame
	void Update () {


		if(canClimb && Input.GetKeyDown(KeyCode.E))
			{
			mainCam.depth = 0;
			climbCam.depth = 1;
			//cc.enabled = false;
			rb.useGravity = false;
			rb.isKinematic = false;
			anim.SetTrigger ("Climb");
			StartCoroutine (afterClimb ());
			}
	}

	IEnumerator afterClimb() {
		yield return new WaitForSeconds (2);
		mainCam.depth = 1;
		climbCam.depth = 0;
		//cc.enabled = true;
		rb.useGravity = true;
		rb.isKinematic = true;
		transform.position = climbCam.transform.position;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ledge") {
			canClimb = true;
			alatetVisible = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		canClimb = false;
		alatetVisible = false;
	}
}
