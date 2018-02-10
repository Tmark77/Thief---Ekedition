using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Collectible {

	public abstract void Collide ();

	protected int kod;

	public override void PickUp(PlayerInventory inv)
	{
		//inv.NewItem (kod);
		inv.NewItem(this);
		gameObject.SetActive (false);
		//Destroy (gameObject);
	}

	public abstract void Use ();
}
