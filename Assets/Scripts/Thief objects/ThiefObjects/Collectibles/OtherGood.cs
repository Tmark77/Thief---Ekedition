using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherGood : Valuable {
	//minden ami nem illik a másik két kategóriába
	public override void Collide()
	{

	}

	public override void PickUp(PlayerInventory inv)
	{
		inv.AddOtherValuable (value);
		Destroy (gameObject);
	}

}
