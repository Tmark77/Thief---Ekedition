using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstract_Material : MonoBehaviour {

	protected AudioClip sound;
	protected AudioSource soundSource;
	//Methods
	public abstract float NoiseGeneration (float noise);
	public abstract bool Soft();
	public abstract bool SeeTrough();
}
