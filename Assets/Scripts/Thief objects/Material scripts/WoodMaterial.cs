using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodMaterial : abstract_Material {
	
	public override float NoiseGeneration (float noise)
	{
		return noise *= 1.5f;
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
