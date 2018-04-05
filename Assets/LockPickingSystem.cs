using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPickingSystem : MonoBehaviour {

    public GameObject lockpicker;

	// Use this for initialization
	void Start () {
        lockpicker.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        

	}

    public static bool canBeLockPick;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "lockpick")
        {
            canBeLockPick = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "lockpick")
        {
            canBeLockPick = false;
        }
    }
}
