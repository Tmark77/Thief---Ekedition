using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : Equipment {

    Rigidbody Rig;

    public override void Use(GameObject hand)
    {
        this.gameObject.SetActive(true);
        Shoot(hand);
    }

    // Use this for initialization
    void Start () {
        kod = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ThiefObject>() != null)
        {
            Debug.Log(other.name);

            ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
            if ((obj as StaticFieldObject).material.Soft())
            {
                Rig = this.gameObject.GetComponent<Rigidbody>();
                Rig.useGravity = false;
                Rig.isKinematic = true;
            }

            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider nearbyObjects in colliders)
            {
                Light_Open g = nearbyObjects.GetComponent<Light_Open>();
                if (g != null)
                {
                    (g as Light_Open).Ignite();
                }
            }
        }
    }


    void Shoot(GameObject hand)
    {
        //GameObject arr = Instantiate(this.gameObject, hand.transform.position, hand.transform.rotation);

        this.gameObject.transform.position = hand.transform.position;
        this.gameObject.transform.rotation = hand.transform.rotation;
        Rig = this.gameObject.GetComponent<Rigidbody>();
        Rig.useGravity = true;
        Rig.isKinematic = false;
        Rig.AddForce(transform.forward * 1500f);
        this.gameObject.transform.Translate(hand.transform.forward, Space.World);
        this.gameObject.SetActive(true);
    }
}
