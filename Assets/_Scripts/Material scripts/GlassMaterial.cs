using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 2f;
	}



	public override bool Soft()
	{
		return false;
	}

	public override bool SeeTrough()
	{
		return true;
	}

	public void Start()
	{
		sound = (AudioClip)Resources.Load("_Sounds/glass/Window Shatter 04", typeof(AudioClip));
	}
		
}
