using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

	int collectedGemsValue;
	int collectedGoldValue;
	int collectedOtherValue;
	List<int> e = new List<int>();
	int i;


	// Use this for initialization
	void Start () {
		collectedGemsValue = 0;
		collectedGoldValue = 0;
		collectedOtherValue = 0;
		i = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("NextItem")) 
		{
			NextItem();
			Debug.Log (e [i]);
		}
		if (Input.GetButtonDown("PrevItem")) 
		{
			PrevItem ();
			Debug.Log (e [i]);
		}
		
	}

	public void NewItem(int item)
	{
		e.Add (item);
		i = e.Count - 1;
		Debug.Log (item);
	}

	public void UseItem()
	{
		//e [i].Use();
		e.RemoveAt (i);
		NextItem ();

	}

	private void NextItem()
	{
		i++;
		if (i >= e.Count)
			i = 0;
	}

	private void PrevItem()
	{
		i--;
		if (i < 0)
			i = e.Count-1;
	}

	public void AddGem (int value)
	{
		collectedGemsValue += value;
	}

	public void AddGold (int value)
	{
		collectedGoldValue += value;
	}

	public void AddOtherValuable (int value)
	{
		collectedOtherValue += value;
	}
}
