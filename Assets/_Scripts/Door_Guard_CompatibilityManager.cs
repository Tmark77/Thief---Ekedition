using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Door_Guard_CompatibilityManager : MonoBehaviour {

	List<GameObject> doors;
	List<Creature> creatures;
	// Use this for initialization
	void Start () {
		
		doors = (GameObject.FindGameObjectsWithTag ("Door")).ToList();
		creatures = GameObject.FindObjectsOfType<Creature> ().ToList();
		//Debug.Log (doors.Count);
		//Debug.Log (creatures.Count);

	}
	
	// Update is called once per frame
	void Update () {
		
		
	}

	public void DoorLockedStatusChanged(int doorID, bool locked)
	{
		Debug.Log ("status changed");
		for (int i = 0; i < creatures.Count; i++) 
		{
			if (locked) 
			{
				int k = -1;
				do 
				{
					k++;
				}
				while(k > creatures[i].e.Count || (creatures[i].e[k] as Equipment).Kod == doorID);

				if(k > creatures[i].e.Count)
				{
					creatures [i].condition.DoorDisable (doorID);
				}
			} 
			else 
			{
				creatures [i].condition.DoorAvaible (doorID);
			}
		}



	}
}
