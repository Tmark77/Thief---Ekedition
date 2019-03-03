using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterArrow : Arrow
{
    public AudioSource hitWood;
    public AudioSource hit;
    public AudioSource water;

    void Start ()
    {
		kod = 1;
        nev = "Water Arrow";
    }
	
    protected override void OnTriggerEnter(Collider other)
    {
		if (IsShooted && other.gameObject.GetComponent<ThiefObject>() != null)
        {
            ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
			NormalArrow a = this.gameObject.AddComponent<NormalArrow>();

			if ((obj as ThiefObject).material.Soft())
            {
                Rig = this.gameObject.GetComponent<Rigidbody>();
                Rig.useGravity = false;
                Rig.isKinematic = true;
                hitWood.Play();
                (obj as ThiefObject).material.NoiseGeneration(1f);
				
				if (obj is Creature)
                {
					a.transform.parent = obj.transform;
				}
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
