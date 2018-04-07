using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashBomb : Equipment {

	Rigidbody Rig;
	public AudioSource ThrowSound;
	public AudioSource ExplosionSound;
	bool IsShooted;
	private float range = 15f; //a range az hogy milyen messze vakít, semmi másra nincs hatással!

	public override bool Use (GameObject hand)
	{
		this.gameObject.SetActive(true);
		Shoot(hand);
		return true;
	}

	void Shoot(GameObject hand)
	{
		ThrowSound.Play();
		this.gameObject.transform.position = hand.transform.position;
		this.gameObject.transform.rotation = hand.transform.rotation;
		Rig = this.gameObject.GetComponent<Rigidbody>();
		Rig.useGravity = true;
		Rig.isKinematic = false;
		Rig.AddForce(transform.forward * 500f);
		this.gameObject.transform.Translate(hand.transform.forward, Space.World);
		this.gameObject.SetActive(true);
		IsShooted = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (IsShooted && other.gameObject.GetComponent<ThiefObject>() != null)
		{
			ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
			ExplosionSound.Play();

			if (obj.GetComponent<Breakable> ()) 
			{
				obj.GetComponent<Breakable> ().Break ();
			}

			Collider[] colliders = Physics.OverlapSphere(transform.position, range); 
			foreach(Collider nearbyObjects in colliders)
			{
				Creature g = nearbyObjects.GetComponent<Creature>();
				//float dist = Vector3.Distance(transform.position, nearbyObjects.transform.position);

				if(g != null)
				{
					if (Vanralatas (g.gameObject.transform)) 
					{
						Vector3 direction = this.gameObject.transform.position - g.gameObject.transform.position;
						float angle = Vector3.Angle (direction, g.gameObject.transform.forward);
						if (g.IsInFieldOfView (angle)) 
						{
							
							g.Blinding ();
						}
					}
				}
			}
				
			IsShooted = false;
			Destroy (this.gameObject);
		}
	}

	bool Vanralatas(Transform obj)
	{
		RaycastHit[] hits = Physics.RaycastAll (obj.position, this.transform.position - obj.position, Vector3.Distance(this.transform.position, obj.position));

		List<ThiefObject> tf = new List<ThiefObject> ();
		foreach (RaycastHit hit in hits) 
		{
			if (hit.collider.gameObject.GetComponent<ThiefObject> () != null) 
			{
				tf.Add (hit.collider.gameObject.GetComponent<ThiefObject> ());
			}
		}

		int ind;
		ind = 0;
		if (tf.Count != 0) {
			do {
				if (!tf[ind].material.SeeTrough())
				{
					return false;
				}
				ind++;
			} while(ind < tf.Count);
		}
		return true;
	}

	// Use this for initialization
	void Start () {
		kod = 6;
		nev = "Flash Bomb";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
