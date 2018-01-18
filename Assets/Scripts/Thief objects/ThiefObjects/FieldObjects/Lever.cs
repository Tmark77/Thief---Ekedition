using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : DynamicFieldObject {

	Animator anim;
	bool used;
	public GameObject lights;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		used = false;
	}

	public override void Interaction ()
	{
		if (used == false) {
			anim.SetTrigger ("off");
			used = true;
			lights.SetActive (false);
		} 
		else 
		{
			anim.SetTrigger ("on");
			used = false;
			lights.SetActive (true);
		}
	}
}
