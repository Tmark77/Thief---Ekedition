using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetNoise : MonoBehaviour {

    public int noiseMeter;
    public Material mat;
    float delay = 10f;
    public static float countdown;

	// Use this for initialization
	void Start () {
        mat.color = Color.black;
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {
        AlertMeter();
	}

    void AlertMeter()
    {
        if(noiseMeter == 0 || countdown <= 0)
        {
            mat.color = Color.black;
        }
        else if(noiseMeter > 0 && noiseMeter <= 5)
        {
            mat.color = Color.yellow;
            countdown -= Time.deltaTime;
        }
        else if(noiseMeter > 5 && noiseMeter <= 10)
        {
            mat.color = Color.red;
            countdown -= Time.deltaTime;
        }
    }

}
