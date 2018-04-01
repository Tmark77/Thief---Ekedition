using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_ProtectedFlame : Light_thief {
	
	public override void Interaction (bool IsRightClicked)
	{
		if (!IsRightClicked) 
		{
			if (IsLit ())
			{
				Extinguish ();
			} 
			else 
			{
				Ignite ();
			}
		}

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
