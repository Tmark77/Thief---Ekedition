﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_OpenFlame_Small : Light_Open {
	
	public override void Interaction (bool IsManualyOperated)
	{
		if (!IsManualyOperated)
        {
            if (IsLit())
            {
                Extinguish();
            }
            else
            {
				Ignite ();
			}
		}
        else
        {
            Extinguish();
        }
    }


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }
}
