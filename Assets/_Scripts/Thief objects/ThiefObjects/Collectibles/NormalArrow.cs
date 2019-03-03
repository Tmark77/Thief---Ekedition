using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalArrow : Arrow
{
    public AudioSource hitWood;
    public AudioSource hit;
    
    void Start ()
    {
		kod = 0;
        nev = "Arrow";
    }
    
    protected override void OnTriggerEnter(Collider other)
    {
		if (IsShooted && other.gameObject.GetComponent<ThiefObject>() != null && !other.isTrigger)
        {
            ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
			if ((obj as ThiefObject).material.Soft())
            {
                Rig = this.gameObject.GetComponent<Rigidbody>();
                Rig.useGravity = false;
                Rig.isKinematic = true;
                hitWood.Play();
                (obj as ThiefObject).material.NoiseGeneration(1f);
                Transform Parent = obj.transform;

                while (Parent.lossyScale != Vector3.one && Parent.parent != null)
                {
                    Parent = Parent.parent;
                }
                if (Parent.lossyScale == Vector3.one)
                    this.transform.parent = obj.transform.parent.transform;

            }
            else
            {  
                if (!played)
                {
                    {
                        hit.Play();
                        (obj as ThiefObject).material.NoiseGeneration(1f);
                        played = true;
                    }
                }
            }

			if (obj is Creature) 
			{
				(obj as Creature).TakeDamage (35);
			}

			if (obj.GetComponent<Breakable> ()) 
			{
				obj.GetComponent<Breakable> ().Break ();
			}
			IsShooted = false;
        }
    }
}
