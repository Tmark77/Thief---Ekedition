using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeHelp : MonoBehaviour {

	private BoxCollider bc;

	// Use this for initialization
	void Start () {
		bc = GetComponent<BoxCollider> ();
		bc.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Climbing.alatetVisible == true) {
			bc.isTrigger = false;
		} else {
			bc.isTrigger = true;
		}
	}
}
