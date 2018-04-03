using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Equipment {
    
    Rigidbody Rig;
    public AudioSource shot;
    public AudioSource hitWood;
    public AudioSource hit;
    bool played;

    // Use this for initialization
    void Start () {
		kod = 0;
        played = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    

	public override void Use (GameObject hand)
	{
        this.gameObject.SetActive(true);
        Shoot(hand);
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
                hitWood.Play();
                //GameObject arrowInst = Instantiate(stabilarrow, arrow.transform.position, arrow.transform.rotation);
                //Destroy(arrowInst, 3600f);
                //Destroy(arrow);
            }
            else
            {
                if (!played)
                {
                    hit.Play();
                    played = true;
                }
            }
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
    }
}
