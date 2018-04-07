using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackjack : Equipment {
	RaycastHit hit;
	float counter;

	public override bool Use (GameObject hand)
	{
		this.gameObject.SetActive (true);
		this.gameObject.GetComponent<MeshRenderer> ().enabled = false;
		this.gameObject.GetComponent<Collider> ().enabled = false;
		if (counter < 0f) {
			counter = 1f;
			if (Physics.Raycast (hand.transform.position, hand.transform.forward, out hit, 3f)) {
				if (hit.collider != null) 
				{
					ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();
					if (obj is Creature) {
						Debug.Log ("Tarkón basztam");
						(obj as Creature).TakeDamage (5);
						(obj as Creature).KnockOut ();
					}
					if (obj.GetComponent<Breakable> ())
					{
						obj.GetComponent<Breakable> ().Break ();
					}
				}
			}
		}
		return false;

	}

	// Use this for initialization
	void Start () {
		counter = 2f;
		kod = 8;
		nev = "Blackjack";
	}
	
	// Update is called once per frame
	void Update () {
		counter -= Time.deltaTime;
	}
}
