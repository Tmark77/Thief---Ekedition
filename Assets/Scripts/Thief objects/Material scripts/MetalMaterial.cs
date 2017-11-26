using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 3f;
	}

	public override bool Soft()
	{
		return false;
	}

	public override bool SeeTrough()
	{
		return false;
	}
}
