using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : DynamicFieldObject {
    
	bool used;
	public GameObject[] dynamicObjects;
	private float[] ranges;
	int i;

	// Use this for initialization
	void Start () {
		used = false;
		ranges = new float[dynamicObjects.Length];
	}

	public override void Interaction (bool IsRightClicked)
	{
		i = 0;
		if (used == false) {
			used = true;
			//lights.SetActive (false);
		} 
		else 
		{
			used = false;
			//lights.SetActive (true);
		}

		foreach (GameObject l in dynamicObjects) {
			l.GetComponent<DynamicFieldObject> ().Interaction(false);
			i++;
		}
	}
}
