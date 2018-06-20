using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Interact : MonoBehaviour {

	[SerializeField] public PlayerInventory inventory;
	public Camera mainCam;
	RaycastHit hit;
	ThiefObject PickedUpCreature;
	[SerializeField]
	public GameObject hand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //if (MenuScript.isGameStarted)
        //{
            Physics.Raycast(hand.transform.position, hand.transform.forward, out hit, 3);

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
						//itt még felvesszük a bácsit
						PickedUpCreature = obj;
						PickedUpCreature.gameObject.SetActive (false);
						//PickedUpCreature.GetComponent<Rigidbody> ().isKinematic = true;
					}
				}
			}
		} 
		else 
		{
			//itt most ledobjuk a bácsit
			PickedUpCreature.gameObject.transform.position = this.gameObject.transform.position+this.gameObject.transform.forward*2;
			this.gameObject.GetComponent<FirstPersonController> ().carriing = false;
			PickedUpCreature.gameObject.SetActive (true);
		}
	}

	void LeftClickInteracting()
	{
		if (!this.gameObject.GetComponent<FirstPersonController> ().carriing) 
		{
			this.gameObject.GetComponent<PlayerInventory> ().UseItem ();
		}
			
	}

}
