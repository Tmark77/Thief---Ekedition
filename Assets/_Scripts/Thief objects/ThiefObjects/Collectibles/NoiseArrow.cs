using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseArrow : Equipment {
	Rigidbody Rig;
	public AudioSource shot;
	public AudioSource hitWood;
	public AudioSource hit;
	bool played;
	bool IsShooted;
	float counter = 0;

	// Use this for initialization
	void Start () {
		kod = 4;
		played = false;
		IsShooted = false;
		nev = "Noise Arrow";
	}

	// Update is called once per frame
	void Update () {

	}


	public override bool Use (GameObject hand)
	{
		this.gameObject.SetActive(true);
		Shoot(hand);
        return true;
    }


	private void OnTriggerEnter(Collider other)
	{
		if (IsShooted && other.gameObject.GetComponent<ThiefObject>() != null)
		{
			ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
			if ((obj as StaticFieldObject).material.Soft())
			{
				Rig = this.gameObject.GetComponent<Rigidbody>();
				Rig.useGravity = false;
				Rig.isKinematic = true;
				hitWood.Play();

			}
			else
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

		float noise = 100f;
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


	void Shoot(GameObject hand)
	{
		played = false;
		shot.Play();
		this.gameObject.transform.position = hand.transform.position;
		this.gameObject.transform.rotation = hand.transform.rotation;
		Rig = this.gameObject.GetComponent<Rigidbody>();
		Rig.useGravity = true;
		Rig.isKinematic = false;
		Rig.AddForce(transform.forward * 1500f);
		this.gameObject.transform.Translate(hand.transform.forward, Space.World);
		this.gameObject.SetActive(true);
		IsShooted = true;
	}
}
