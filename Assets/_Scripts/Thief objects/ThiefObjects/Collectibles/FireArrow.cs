using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : Arrow
{
    public AudioSource hitWood;
    public AudioSource hit;
    public AudioSource fire;
 
    void Start () {
        kod = 2;
		nev = "Fire Arrow";
    }
    
    protected override void OnTriggerEnter(Collider other)
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
                (obj as ThiefObject).material.NoiseGeneration(1f);
            }
            else
            {
                if (!played)
                {
                    hit.Play();
                    (obj as ThiefObject).material.NoiseGeneration(1f);
                    played = true;
                }
            }

            fire.Play();
			NormalArrow a = this.gameObject.AddComponent<NormalArrow>();

            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
            foreach (Collider nearbyObjects in colliders)
            {
                Light_Open g = nearbyObjects.GetComponent<Light_Open>();
                if (g != null)
                {
                    (g as Light_Open).Ignite();
                }
            }

			if (obj is Creature) 
			{
				(obj as Creature).TakeDamage (80);
				a.transform.parent = obj.transform;
			}
			if (obj.GetComponent<Breakable> ())
			{
				obj.GetComponent<Breakable> ().Break ();
			}
				
            a.material = this.gameObject.GetComponent<abstract_Material>();
            a.shot = this.shot;
            a.hit = this.hit;
            a.hitWood = this.hitWood;
            this.gameObject.GetComponent<Renderer>().material = Resources.Load("black", typeof(Material)) as Material;
			IsShooted = false;
			Destroy(this);
        }
    }
}
