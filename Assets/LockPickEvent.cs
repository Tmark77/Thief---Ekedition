using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickEvent : MonoBehaviour {

    Transform startPoz;

	// Use this for initialization
	void Start () {
        startPoz = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        
        startPoz.Translate((Mathf.Sin(Time.time * 2) / 200f), 0f, 0f, Space.Self);
        //this.transform.Translate((Mathf.Sin(Time.time*2) / 200f), 0f, 0f, Space.Self);

    }

}
