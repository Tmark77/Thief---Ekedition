using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickEvent : MonoBehaviour {
    

    // Use this for initialization
    void Awake ()
    {
        
    }
	
	// Update is called once per frame
	void Update ()
    { 
        this.transform.Translate(Mathf.Sin(Time.time * 3) / 120f, 0f, 0f, Space.Self);
    }
}
