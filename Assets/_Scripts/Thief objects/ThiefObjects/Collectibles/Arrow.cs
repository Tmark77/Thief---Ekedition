using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Equipment {

    public GameObject stabilarrow;
    public GameObject arrow;

    // Use this for initialization
    void Start () {
		kod = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public override void Collide()
	{

    }

	public override void Use ()
	{
		throw new System.NotImplementedException ();
	}


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ThiefObject>() != null)
        {
            ThiefObject obj = other.gameObject.GetComponent<ThiefObject>();
            if ((obj as StaticFieldObject).material.Soft())
            {
                GameObject arrowInst = Instantiate(stabilarrow, arrow.transform.position, arrow.transform.rotation);
                Destroy(arrowInst, 3600f);
                Destroy(arrow);
            }
        }
    }
}
