using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherGood : Valuable {
	//minden ami nem illik a másik két kategóriába
	public override void Collide()
	{

	}

	public override void PickUp()
	{
		//Add to otherGoods
		Destroy (gameObject);
	}

}
