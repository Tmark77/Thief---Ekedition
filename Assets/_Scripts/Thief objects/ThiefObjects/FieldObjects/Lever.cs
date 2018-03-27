using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : DynamicFieldObject {

	Animator anim;
	bool used;
	public Light[] lights;
	private float[] ranges;
	int i;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		used = false;
		ranges = new float[lights.Length];
	}

	public override void Interaction ()
	{
		i = 0;
		if (used == false) {
			anim.SetTrigger ("off");
			used = true;
			//lights.SetActive (false);
			foreach (Light l in lights) {
				ranges [i] = l.range;
				l.range = 0;
				i++;
			}
		} 
		else 
		{
			anim.SetTrigger ("on");
			used = false;
			//lights.SetActive (true);
			foreach (Light l in lights) {
				l.range = ranges [i];
				i++;
			}
		}
	}
}
