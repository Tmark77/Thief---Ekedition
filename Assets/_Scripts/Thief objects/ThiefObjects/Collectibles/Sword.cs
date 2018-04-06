using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Equipment {
	RaycastHit hit;
	float counter;

	public override bool Use (GameObject hand)
	{
		this.gameObject.SetActive (true);
		this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		this.gameObject.GetComponent<Collider> ().enabled = false;
		if (counter < 0f) 
		{
			counter = 2f;
			if (Physics.Raycast (hand.transform.position, hand.transform.forward, out hit, 3f)) 
			{
				if (hit.collider != null) 
				{
					ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();
					if (obj is Creature) 
					{
						Debug.Log ("Belevertem");
						(obj as Creature).TakeDamage (35);
					}
				}
			}
		}

		//this.gameObject.SetActive (false);
		return false;

	}

	// Use this for initialization
	void Start () 
	{
		kod = 9;
		counter = 2f;
		nev = "Sword";
	}

	// Update is called once per frame
	void Update () 
	{
		counter -= Time.deltaTime;
	}
}
