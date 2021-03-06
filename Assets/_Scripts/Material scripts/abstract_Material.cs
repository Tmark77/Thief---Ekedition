﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class abstract_Material : MonoBehaviour {

	protected AudioClip sound;
	//protected AudioSource soundSource;
	//Methods
	public abstract float NoiseGeneration (float noise);
	public abstract bool Soft();
	public abstract bool SeeTrough();

    public AudioClip MaterialSound()
    {
        return sound;
    }
}
