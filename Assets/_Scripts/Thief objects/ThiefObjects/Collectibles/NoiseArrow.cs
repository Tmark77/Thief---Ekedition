using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseArrow : Arrow
{
	
	public AudioSource hitWood;
	public AudioSource hit;
	
	float counter = 0;

	void Start ()
    {
        kod = 4;
		nev = "Noise Arrow";
	}

	
	protected override void OnTriggerEnter(Collider other)
	{
		if (IsShooted && other.gameObject.GetComponent<ThiefObject>() != null)
		{
			ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
			{
				if (!played)
				{
					hit.Play();
					played = true;
				}
			}
			if (obj.GetComponent<Breakable> ())
			{
				obj.GetComponent<Breakable> ().Break ();
			}
			IsShooted = false;
			counter = 0;
			InvokeRepeating ("Active", 0f, 1f);
		}
	}

	void Active()
	{
		counter++;
		if (counter == 5) 
		{
			CancelInvoke ("Active");
		}

		float noise = 30f;
		Collider[] colliders = Physics.OverlapSphere(transform.position, noise);

		foreach(Collider nearbyObjects in colliders)
		{
			Creature g = nearbyObjects.GetComponent<Creature>();
			float dist = Vector3.Distance(transform.position, nearbyObjects.transform.position);

			if(g != null)
			{
				if(dist < noise)
				{
					g.GetNoise((int)(((noise-dist)*100/noise)*1),this.gameObject.transform.position); //1 a gyanú pontok szorzója

				}
			}
		}
	}
}
