using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : Collectible {

	public abstract void Collide ();

	[SerializeField]
	public int value;
}
