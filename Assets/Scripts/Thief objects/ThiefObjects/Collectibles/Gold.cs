using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : Valuable {
	//minden nemesfém ide tartozik
	public override void Collide()
	{

	}

	public override void PickUp()
	{
		//Add to Gold
		Destroy (gameObject);
	}


}
