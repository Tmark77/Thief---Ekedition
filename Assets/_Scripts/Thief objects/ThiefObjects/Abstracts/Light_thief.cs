using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Light_thief : DynamicFieldObject {

	protected float lightrange;

	public void Extinguish()
	{
		if(GetComponentInParent<Light> ().range != 0)
        {
            lightrange = GetComponentInParent<Light>().range;
            GetComponentInParent<Light>().range = 0;
        }
	}

	public void Ignite()
	{
        if (GetComponentInParent<Light>().range == 0)
        {
            GetComponentInParent<Light>().range = lightrange;
        }
	}
	/// <summary>
	/// Determines whether this instance is lit.
	/// </summary>
	/// <returns><c>true</c> returns false if this light's range is 0, otherwise true <c>false</c>.</returns>
	public bool IsLit()
	{
		return GetComponentInParent<Light> ().range != 0;	
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
