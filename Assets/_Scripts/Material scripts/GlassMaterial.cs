using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassMaterial : abstract_Material {

	public override float NoiseGeneration (float noise)
	{
		return noise *= 2f;
	}



	public override bool Soft()
	{
		return false;
	}

	public override bool SeeTrough()
	{
		return true;
	}


    public GameObject shatteredGlassWall;
    public GameObject glasswall;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Equipment>())
        {
            Instantiate(shatteredGlassWall, this.transform.position, this.transform.rotation);
            Destroy(glasswall);
        }
    }
}
