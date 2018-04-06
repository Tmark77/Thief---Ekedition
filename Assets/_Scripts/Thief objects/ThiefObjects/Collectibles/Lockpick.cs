﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpick : Equipment {


    RaycastHit hit;
    public GameObject lockpicker;

    public override bool Use(GameObject hand)
    {
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Collider>().enabled = false;
        
        if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, 3f))
        {
            if (hit.collider != null)
            {
                ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject>();
				if (obj is Door && (obj as Door).locked)
                {
					if ((obj as Door).canBeLockPicked) 
					{
						StartCoroutine (LockpickRoutine ());
					}
					else
					{
						Debug.Log ("This lock is not conventional. I need to find antoher way to unlock it.");
					}
                    
                }
            }
        }

        return false;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(passes);

        if (passes == 3)
        {
            successLockPicking = true;
        }

        if (successLockPicking)
        {
            Debug.Log("nyitva");
			if(hit.collider.gameObject.GetComponent<Door>())
            	hit.collider.gameObject.GetComponent<Door>().locked = false;
			passes = 0;
			successLockPicking = false;
            lockpicker.SetActive(false);

        }
    }

    int passes;
	bool successLockPicking;

    public IEnumerator LockpickRoutine()
    {
        lockpicker.SetActive(true);
        passes = 0;

        while (passes < 3)
        {
            if (LockPickingSystem.canBeLockPick && Input.GetKeyDown(KeyCode.H))
            {
                passes++;
                yield return new WaitForSecondsRealtime(1);
            }
            yield return null;
        }
    }
}
