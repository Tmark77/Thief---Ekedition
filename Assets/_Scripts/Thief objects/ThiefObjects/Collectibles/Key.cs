using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Equipment
{
    [Range(10,20)]
    public int keyID;

    public override bool Use(GameObject hand)
    {
        RaycastHit hit;
        if (Physics.Raycast(hand.transform.position,hand.transform.forward,out hit))
        {
            Door obj = hit.collider.GetComponent<Door>();
            if (obj != null)
            {
                if ((obj as Door).unlocked == false && (obj as Door).keyID == this.keyID)
                {
                    (obj as Door).unlocked = true;
                }
            }
        }
        return false;
    }

    // Use this for initialization
    void Start () {
        kod = keyID;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
