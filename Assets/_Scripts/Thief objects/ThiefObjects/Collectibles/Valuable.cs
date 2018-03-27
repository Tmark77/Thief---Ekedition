using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Valuable : Collectible {

	public abstract void Collide ();

	public int value;
}
