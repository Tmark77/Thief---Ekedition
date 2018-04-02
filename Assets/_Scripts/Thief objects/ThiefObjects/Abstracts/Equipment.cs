using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Collectible {
    

	protected int kod;

	public int Kod
	{
		get{ return kod; }
	}

	public override void PickUp(PlayerInventory inv)
	{
        //inv.NewItem (kod);
		inv.NewItem(this);
		gameObject.SetActive (false);
		//Destroy (gameObject);
	}

	public abstract void Use (GameObject hand);
}
