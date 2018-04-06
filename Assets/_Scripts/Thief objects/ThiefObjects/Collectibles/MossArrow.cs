using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossArrow : Equipment
{
    Rigidbody Rig;
    public GameObject plane;
    public AudioSource shot;
    public AudioSource moss;
	bool IsShooted;

    public override bool Use(GameObject hand)
    {
        this.gameObject.SetActive(true);
        Shoot(hand);
        return true;
    }

    // Use this for initialization
    void Start () {
        kod = 3;
		IsShooted = false;
		nev = "Moss Arrow";
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
		if (IsShooted && other.gameObject.GetComponent<ThiefObject>() != null)
        {
            moss.Play();

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    Instantiate(plane, new Vector3(this.transform.position.x + i, this.transform.position.y, this.transform.position.z + j), plane.transform.rotation);
                }
            }
            
            Destroy(this.gameObject);
			IsShooted = false;
        }
    }


    void Shoot(GameObject hand)
    {
        //GameObject arr = Instantiate(this.gameObject, hand.transform.position, hand.transform.rotation);
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
