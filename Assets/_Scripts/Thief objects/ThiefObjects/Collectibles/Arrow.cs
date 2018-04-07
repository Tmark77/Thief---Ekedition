﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Equipment {
    
    Rigidbody Rig;
    public AudioSource shot;
    public AudioSource hitWood;
    public AudioSource hit;
    public AudioSource breaking;
    bool played;
	bool IsShooted;

    // Use this for initialization
    void Start () {
		kod = 0;
        played = false;
		IsShooted = false;
		nev = "Arrow";
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
			if ((obj as ThiefObject).material.Soft())
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
                    {
                        hit.Play();
                        played = true;
                    }
                }
            }

			if (obj is Creature) 
			{
				(obj as Creature).TakeDamage (50);
			}

			if (obj.GetComponent<Breakable> ()) 
			{
				obj.GetComponent<Breakable> ().Break ();
			}

			IsShooted = false;
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
