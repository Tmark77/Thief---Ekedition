using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Valuable {

	public override void Collide()
	{
		
	}

	public override void PickUp()
	{
		//Add to Gem
		Destroy (gameObject);
	}


}
