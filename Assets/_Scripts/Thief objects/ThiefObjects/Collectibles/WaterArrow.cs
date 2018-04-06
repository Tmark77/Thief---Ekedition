using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArrow : Equipment {

    Rigidbody Rig;
    public AudioSource shot;
    public AudioSource hitWood;
    public AudioSource hit;
    public AudioSource water;
    bool played;
	bool IsShooted;

    // Use this for initialization
    void Start () {
		kod = 1;
        played = false;
		IsShooted = false;
		nev = "Water Arrow";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
		
    public override bool Use(GameObject hand)
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

            water.Play();

            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider nearbyObjects in colliders)
            {
                Light_Open g = nearbyObjects.GetComponent<Light_Open>();
                if (g != null)
                {
                    (g as Light_Open).Extinguish();
                }
            }

            Arrow a = this.gameObject.AddComponent<Arrow>();
            a.material = this.gameObject.GetComponent<abstract_Material>();
            a.shot = this.shot;
            a.hit = this.hit;
            a.hitWood = this.hitWood;
            this.gameObject.GetComponent<Renderer>().material = Resources.Load("black", typeof(Material)) as Material;
			IsShooted = false;
			Destroy(this);
        }
    }


    void Shoot(GameObject hand)
    {
        //GameObject arr = Instantiate(this.gameObject, hand.transform.position, hand.transform.rotation);
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
