using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothMaterial : abstract_Material {

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

	public void Start()
	{
		sound = (AudioClip)Resources.Load("_Sounds/steps/Footsteps Linoleum Slow 02", typeof(AudioClip));
	}

}
