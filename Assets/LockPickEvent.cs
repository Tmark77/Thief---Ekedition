using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickEvent : MonoBehaviour {

    public GameObject start;
    public GameObject end;
    bool irany;

    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (this.transform.position == end.transform.position)
        {
            irany = false;
        }
        else if (this.transform.position == start.transform.position)
        {
            irany = true;
        }

        if (irany)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, end.transform.position, Time.deltaTime / 5f);
        }
        else
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, start.transform.position, Time.deltaTime / 5f);
        }
    }


}
