using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Collectible {

	public abstract void Collide ();

	public override void PickUp()
	{
		Destroy (gameObject);
		//Add to inventory
	}
}
