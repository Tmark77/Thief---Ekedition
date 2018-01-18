using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Collectible {

	public abstract void Collide ();

	public override void PickUp(PlayerInventory inv)
	{
		inv.NewItem (this.GetComponent<Equipment>());
		gameObject.SetActive (false);
		Destroy (gameObject);
	}

	public abstract void Use ();
}
