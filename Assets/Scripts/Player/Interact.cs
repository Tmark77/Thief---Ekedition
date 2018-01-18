using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

	[SerializeField] public PlayerInventory inventory;
	public Camera mainCam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1)) {
			Interacting ();
		}
	}

	void Interacting()
	{
		RaycastHit hit;

		if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward,out hit,3)) {
			
			ThiefObject obj = hit.collider.gameObject.GetComponent<ThiefObject> ();

			if (obj is Collectible) {
				(obj as Collectible).PickUp (inventory);
			}

			if (obj is DynamicFieldObject) {
				(obj as DynamicFieldObject).Interaction ();
			}
		}
	}

}
