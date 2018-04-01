using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : DynamicFieldObject {

	Animator anim;
	bool opened;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		opened = false;
	}
	
	public override void Interaction (bool IsRightClicked)
	{
		if (opened == false) {
			anim.SetTrigger ("open");
			opened = true;
		} 
		else 
		{
			anim.SetTrigger ("close");
			opened = false;
		}
	}
}
