using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClotheMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 0.5f;
	}

	public override bool Soft()
	{
		return true;
	}

	public override bool SeeTrough()
	{
		return false;
	}
}
