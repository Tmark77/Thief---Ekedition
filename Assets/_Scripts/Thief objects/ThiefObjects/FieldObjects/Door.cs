using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : DynamicFieldObject {

	Animator anim;
	bool opened;
    public bool unlocked; //true open, false close
    Quaternion newRot;

    [Range(10, 20)]
    public int keyID;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		opened = false;
	}
	
	public override void Interaction (bool IsRightClicked)
	{
		if (opened == false && unlocked)
        {
			opened = true;
        } 
		else 
		{
            opened = false;
		}
	}

    private void Update()
    {
        if (opened)
        {
            newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
        else
        {
            newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 200);
            transform.rotation = newRot;
        }
    }
}
