using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	[SerializeField] public PlayerInventory inventory;
	public Camera mainCam;
	RaycastHit hit;	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(1)) {
			RightClickInteracting ();
		}
		if (Input.GetMouseButtonDown(0)) {
			LeftClickInteracting ();
		}
	}

	void RightClickInteracting()
	{
		if (hit.collider != null) {
			
			ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();

			if (obj is Collectible) {
				(obj as Collectible).PickUp (inventory);
			}

			if (obj is DynamicFieldObject) {
				(obj as DynamicFieldObject).Interaction ();
			}

			if (obj is Creature) {
				/*Player.carriing = */(obj as Creature).Carry ();
			}
		}
	}

	void LeftClickInteracting()
	{
		if (hit.collider != null) 
		{
			ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();

			if (obj is Creature) 
			{
				(obj as Creature).TakeDamage(10);
				(obj as Creature).KnockOut();
			}
		}
	
	}

}
