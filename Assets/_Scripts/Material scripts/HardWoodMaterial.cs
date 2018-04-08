using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardWoodMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 2f;
	}

	public override bool Soft()
	{
		return true;
	}

	public override bool SeeTrough()
	{
		return false;
	}

	public void Start()
	{
		sound = (AudioClip)Resources.Load("_Sounds/steps/Footsteps Hard Surface", typeof(AudioClip));
	}
}
