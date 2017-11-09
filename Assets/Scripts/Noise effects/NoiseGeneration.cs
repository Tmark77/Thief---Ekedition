using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGeneration : MonoBehaviour {

	bool isGrounded;
    public int radius;
    public int noise;

	// Use this for initialization
	void Start () {
		isGrounded = false;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Noise()
	{
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObjects in colliders)
        {
            GetNoise g = nearbyObjects.GetComponent<GetNoise>();
            float dist = Vector3.Distance(transform.position, nearbyObjects.transform.position);
            if(g != null)
            {
                GetNoise.countdown = 10f;
                if(dist < radius && dist > radius / 2)
                {
                    g.noiseMeter = noise / 2;
                }
                else
                {
                    g.noiseMeter = noise;
                }
            }
        }
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Floor" && isGrounded == false)
		{
			Noise();
            isGrounded = true;
        }
	}

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }
}
