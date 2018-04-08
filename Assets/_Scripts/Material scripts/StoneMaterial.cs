using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 1.5f;
	}

	public override bool Soft()
	{
		return false;
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
