using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 4f;
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
		sound = (AudioClip)Resources.Load("_Sounds/arrow/Arrow Hit", typeof(AudioClip));
	}
}
