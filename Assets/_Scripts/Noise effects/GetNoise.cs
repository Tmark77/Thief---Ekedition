using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNoise : MonoBehaviour {

    public int noiseMeter;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// ez csak a testlevelhez kell 
		if(noiseMeter >=50)
		GetComponent<Renderer> ().material.color = Color.red;
	}
		

}
