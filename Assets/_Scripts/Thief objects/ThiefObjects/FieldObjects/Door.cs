using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : DynamicFieldObject {

	Animator anim;
	bool opened;
    public bool locked; //true closed, false opened
    public bool canBeLockPicked;
    Quaternion newRot;
	public GameObject door02;

    [Range(10, 20)]
    public int keyID;

    // Use this for initialization
    void Start () {
		anim = GetComponent<Animator> ();
		opened = false;
	}
	
	public override void Interaction (bool IsRightClicked)
	{
        if (IsRightClicked)
        {
            if (opened == false && !locked)
            {
                opened = true;
            }
            else
            {
                opened = false;
            }
        }
        else
        {
            opened = true;
        }
		
	}

    private void Update()
    {
        if (opened)
        {
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 90.0f, 0.0f), Time.deltaTime * 100);
            transform.rotation = newRot;
            if (this.gameObject.transform.rotation.y == 90.0f)
            {
                //this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
        else
        {
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            newRot = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0.0f, 0.0f, 0.0f), Time.deltaTime * 100);
            transform.rotation = newRot;
            if (this.gameObject.transform.rotation.y == 0f)
            {
                //this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
    }
}
