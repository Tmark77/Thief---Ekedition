using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Interact : MonoBehaviour
{

	[SerializeField] public PlayerInventory inventory;
	public Camera mainCam;
	RaycastHit hit;
	ThiefObject PickedUpCreature;
	[SerializeField]
	public GameObject hand;
	ThiefObject LastLookedCollectible; //ez a név szar

	void Start ()
    {
		
	}
	
	void FixedUpdate () 
	{

        //if (MenuScript.isGameStarted)
        //{
            Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, 3);
		if (hit.collider != null && hit.collider.gameObject.GetComponent<ThiefObject> () is I_Highlightable) 
		{
			ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();
			if (obj != LastLookedCollectible) 
			{
				if (LastLookedCollectible != null) 
				{
					(LastLookedCollectible as I_Highlightable).DeHighlight ();
					LastLookedCollectible = null;
				}
				LastLookedCollectible = obj;
				(obj as I_Highlightable).Highlight ();
			}
		}
		else 
		{
			if (LastLookedCollectible != null) 
			{
				(LastLookedCollectible as I_Highlightable).DeHighlight ();
				LastLookedCollectible = null;
			}
		}
        if (Input.GetMouseButtonDown(1))
        {
            RightClickInteracting();
        }
        if (Input.GetMouseButtonDown(0))
        {
            LeftClickInteracting();
        }
        //}
	}

	void RightClickInteracting()
	{
		if (!this.gameObject.GetComponent<FirstPersonController> ().carriing) {
			if (hit.collider != null) {
			
				ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();

				if (obj is Collectible) {
					(obj as Collectible).PickUp (inventory);
				}

				if (obj is DynamicFieldObject) {
					(obj as DynamicFieldObject).Interaction (true);
				}

				if (obj is Creature) 
				{
					this.gameObject.GetComponent<FirstPersonController> ().carriing = (obj as Creature).Carry ();
					if (this.gameObject.GetComponent<FirstPersonController> ().carriing) 
					{
						//itt felvesszük a bácsit
						PickedUpCreature = obj;
						PickedUpCreature.transform.parent.gameObject.SetActive (false);
						//PickedUpCreature.GetComponent<Rigidbody> ().isKinematic = true;
					}
				}
			}
		} 
		else 
		{
			//itt most ledobjuk a bácsit
			PickedUpCreature.transform.parent.position = this.gameObject.transform.position+this.gameObject.transform.forward*2;
			this.gameObject.GetComponent<FirstPersonController> ().carriing = false;
            PickedUpCreature.transform.parent.gameObject.SetActive (true);
		}
	}

	void LeftClickInteracting()
	{
		if (!this.gameObject.GetComponent<FirstPersonController> ().carriing) 
		{
            try
            {
                this.gameObject.GetComponent<PlayerInventory>().UseItem();
            }
            catch //hát ja, ennek nem try-catch-ben kéne lenni.
            {
                Debug.Log("You don't have any item to use.");
            }
		}
			
	}

}
