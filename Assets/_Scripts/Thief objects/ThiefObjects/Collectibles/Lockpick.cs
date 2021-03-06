﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lockpick : Equipment {

    public static bool lockPickInProgress;
    RaycastHit hit;
    public GameObject lockpicker;
    public AudioSource kattanas;

    public override bool Use(GameObject hand)
    {
        if (!this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(true);
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<Collider>().enabled = false;

            if (Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, 3f))
            {
                if (hit.collider != null)
                {
                    ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject>();
                    if (obj is Door && (obj as Door).Locked)
                    {
                        if ((obj as Door).canBeLockPicked)
                        {
							lockPickInProgress = true;
                            StartCoroutine(LockpickRoutine());
                        }
                        else
                        {
                            Debug.Log("This lock is not conventional. I need to find antoher way to unlock it.");
                        }

                    }
                }
            }
        }
        else
        {
            lockPickInProgress = false;
            this.gameObject.SetActive(false);
            //this.lockpicker.SetActive(false);
            lockpicker.transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        return false;
    }

    // Use this for initialization
    void Start () {
		kod = 7;
		nev = "Lockpicks";
        lockPickInProgress = false;
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
            	hit.collider.gameObject.GetComponent<Door>().Locked = false;
			passes = 0;
			successLockPicking = false;
            //lockpicker.SetActive(false);
            lockpicker.transform.localPosition = new Vector3(0f, 0f, 0f);
            lockPickInProgress = false;
        }
    }

    int passes;
	bool successLockPicking;

    public IEnumerator LockpickRoutine()
    {
        //this.lockpicker.SetActive(true);
        lockpicker.transform.localPosition = new Vector3(0f, 0f, 0.25f);
        passes = 0;

        while (passes < 3)
        {
            if (LockPickingSystem.canBeLockPick && Input.GetMouseButtonDown(1))
            {
                passes++;
                kattanas.Play();
                yield return new WaitForSecondsRealtime(1);
            }
            yield return null;
        }
    }
}
