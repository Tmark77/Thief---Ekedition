using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArrow : Equipment {

	// Use this for initialization
	void Start () {
		kod = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		

	public override void Collide ()
	{}

	public override void Use ()
	{
		throw new System.NotImplementedException ();
	}

}
