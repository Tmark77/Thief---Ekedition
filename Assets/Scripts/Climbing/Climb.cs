using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour {

    public Transform player;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Ledge")
        {
            if(Input.GetButtonDown("Jump"))
            {
                player.position += new Vector3(0, 2, 0);
            }
        }
    }

}
