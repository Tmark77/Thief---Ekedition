using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : DynamicFieldObject {

	bool opened;
    public bool locked; //true closed, false opened
    public bool canBeLockPicked;
    Quaternion newRot;
	public GameObject door02;
    public AudioSource openingAudio;
    public AudioSource closingingAudio;
	private float baseRotation;

    [Range(10, 20)]
    public int keyID;

    // Use this for initialization
    void Start () {
		opened = false;
		baseRotation = this.transform.parent.eulerAngles.y;
	}
	
	public override void Interaction (bool IsManualyOperated)
	{
		if (IsManualyOperated)
        {
            if (opened == false && !locked)
            {
                opened = true;
                openingAudio.Play();
            }
            else
            {
                closingingAudio.Play();
                opened = false;
            }
        }
        else
        {
			opened = !opened;
			if(opened)
            	openingAudio.Play();
			else
				closingingAudio.Play();
            
        }

		//if (opened) {
		//	door02.GetComponent<Collider> ().isTrigger = true;
		//} else {
		//	door02.GetComponent<Collider> ().isTrigger = false;
		//}
		
	}

    private void Update()
    {
        if (opened)
        {
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
			newRot = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.Euler(0.0f, baseRotation + 90.0f, 0.0f), Time.deltaTime * 100);
			transform.parent.rotation = newRot;
            
        }
        else
        {
            //this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
			newRot = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.Euler(0.0f, baseRotation, 0.0f), Time.deltaTime * 100);
			transform.parent.rotation = newRot;
           
        }
    }
}
