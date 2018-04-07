using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {

	public GameObject shatteredObject;

	public void Break()
	{
		//ez csak a zajongás
		float noise = 100f;
		Collider[] colliders = Physics.OverlapSphere(transform.position, noise);

		foreach(Collider nearbyObjects in colliders)
		{
			Creature g = nearbyObjects.GetComponent<Creature>();
			float dist = Vector3.Distance(transform.position, nearbyObjects.transform.position);
			//Debug.Log (noise);
			if(g != null)
			{
				if(dist < noise)
				{
					g.GetNoise((int)(((noise-dist)*100/noise)*1),this.gameObject.transform.position); //1 a gyanú pontok szorzója
					//Debug.Log(noise);
				}
			}
		}
		//idáig

		//ez az hogy összetörik
		Instantiate(shatteredObject, this.transform.position, this.transform.rotation);
		Destroy(this.gameObject);

	}
		
}
