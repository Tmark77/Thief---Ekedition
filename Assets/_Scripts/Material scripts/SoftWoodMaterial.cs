﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoftWoodMaterial : abstract_Material {
	
	public override float NoiseGeneration (float noise)
	{
		return noise *= 1f;
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