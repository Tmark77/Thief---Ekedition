using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class abstract_Material : MonoBehaviour {

	//Methods
	public abstract float NoiseGeneration (float noise);
	public abstract bool Soft();
	public abstract bool SeeTrough();
}
