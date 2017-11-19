using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeekL : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Wall") {
			UnityStandardAssets.Characters.FirstPerson.FirstPersonController.canPeekL = true;		
		}
	}

	void OnTriggerExit()
	{
		UnityStandardAssets.Characters.FirstPerson.FirstPersonController.canPeekL = false;	
	}
}
