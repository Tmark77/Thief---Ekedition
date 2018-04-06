using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Equipment : Collectible {

	protected int kod;

	public int Kod
	{
		get{ return kod; }
	}

	protected string nev;

	public string Nev
	{
		get { return nev; }
	}


	public override void PickUp(PlayerInventory inv)
	{
        //inv.NewItem (kod);
		inv.NewItem(this);
		gameObject.SetActive (false);
		//Destroy (gameObject);
	}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="hand"></param>
    /// <returns> Visszaadja azt, hogy elhasználódott-e az item, a hazsnálat során. </returns>
	public abstract bool Use (GameObject hand);
}
