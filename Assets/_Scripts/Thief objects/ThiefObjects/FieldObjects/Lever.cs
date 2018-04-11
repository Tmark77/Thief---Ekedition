using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : DynamicFieldObject {
    
	bool used;
	public DynamicFieldObject[] dynamicObjects;
	int i;

	// Use this for initialization
	void Start () {
		used = false;
	}

	public override void Interaction (bool IsManualyOperated)
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

		foreach (DynamicFieldObject l in dynamicObjects) {
			l.Interaction(false);
			i++;
		}
	}
}
