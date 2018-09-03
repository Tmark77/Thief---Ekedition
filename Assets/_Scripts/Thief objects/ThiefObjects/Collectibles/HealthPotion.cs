using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Equipment {


	#region implemented abstract members of Equipment
	public override bool Use (GameObject hand)
	{
		hand.transform.parent.parent.GetComponent<PlayerHealth> ().GetHeal (50);
		return true;
	}
	#endregion

	// Use this for initialization
	void Start () {
		kod = 9;
		nev = "Health Potion";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
