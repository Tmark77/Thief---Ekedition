using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossArrow : Arrow
{
    public GameObject plane;
    public AudioSource moss;
	
    void Start ()
    {
        kod = 3;
		nev = "Moss Arrow";
    }
	
    protected override void OnTriggerEnter(Collider other)
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

}
