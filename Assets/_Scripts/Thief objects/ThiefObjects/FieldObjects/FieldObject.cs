using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FieldObject : ThiefObject {

	public float Collide (float noise)
	{
		return material.NoiseGeneration (noise);
	}
}
