using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : Valuable {

	public override void Collide()
	{
		
	}

	public override void PickUp(PlayerInventory inv)
	{
		inv.AddGem (value);
		Destroy (gameObject);
	}


}
