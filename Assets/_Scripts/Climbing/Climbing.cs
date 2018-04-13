using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Climbing : MonoBehaviour {

	private bool canClimb;
	public Animator anim;
	public Camera climbCam;
	public Camera mainCam;
	public Transform playerObject;
	public GameObject lightGemObject;
	public GameObject hand;

	public static bool alatetVisible;

    public static bool climbingIsInProgress;

	// Use this for initialization
	void Start () {
        climbingIsInProgress = false;
    }
	
	// Update is called once per frame
	void Update () {

		if (Climbing.climbingIsInProgress)
		{
			lightGemObject.SetActive(false);
			hand.SetActive(false);
		}
		else
		{
			lightGemObject.SetActive(true);
			hand.SetActive(true);
		}
		if(canClimb && Input.GetKey(GameManager.GM.climb))
			{
            climbingIsInProgress = true;
            mainCam.depth = 0;
			climbCam.depth = 1;
			MouseLook.XSensitivity = 0;
			MouseLook.YSensitivity = 0;
			anim.SetTrigger ("Climb");
			StartCoroutine (afterClimb ());
			}
	}

	IEnumerator afterClimb() {
		yield return new WaitForSeconds (2);
		mainCam.depth = 1;
		climbCam.depth = 0;
		MouseLook.XSensitivity = 2;
		MouseLook.YSensitivity = 2;
		anim.SetTrigger ("NoClimb");
		playerObject.position = climbCam.transform.position;
        climbingIsInProgress = false;
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
