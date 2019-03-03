using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Arrow : Equipment
{
    protected Rigidbody Rig;
    protected bool played = false;
    public AudioSource shot;
    protected bool IsShooted = false;
    
    public override bool Use(GameObject hand)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Pull(hand));
        return true;
    }

    IEnumerator Pull(GameObject hand)
    {
        int shootforce = 100;
        float t = Time.time;
        yield return new WaitWhile(() => Input.GetMouseButton(0));

        t = Time.time - t;
        shootforce += (int)(750 * t);
        if (shootforce > 1500)
        {
            shootforce = 1500;
        }

        Shoot(hand, shootforce);
        yield return null;
    }

    void Shoot(GameObject hand, int shootforce)
    {
        played = false;
        shot.Play();
        this.gameObject.transform.position = hand.transform.position;
        this.gameObject.transform.rotation = hand.transform.rotation;
        Rig = this.gameObject.GetComponent<Rigidbody>();
        Rig.useGravity = true;
        Rig.isKinematic = false;
        Rig.AddForce(transform.forward * shootforce);
        //Rig.AddForce(transform.forward * 1500f);
        this.gameObject.transform.Translate(hand.transform.forward, Space.World);
        this.gameObject.SetActive(true);
        IsShooted = true;
    }

    abstract protected void OnTriggerEnter(Collider other);
}
