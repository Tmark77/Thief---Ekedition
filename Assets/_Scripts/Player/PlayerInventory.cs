using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public GameObject hand;

	int collectedGemsValue;
	int collectedGoldValue;
	int collectedOtherValue;

	public Text goldValue;
	public Text gemValue;
	public Text otherValue;

	//List<int> e = new List<int>();
	List<Equipment> e = new List<Equipment>();
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
		}
		if (Input.GetButtonDown("PrevItem")) 
		{
			PrevItem ();
		}

        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }

		//goldValue.text = "Gold: " + collectedGoldValue;
		//
		//gemValue.text = "Gem: " + collectedGemsValue;
		//
		//otherValue.text = "Other Goods: " + collectedOtherValue;
	}

	//public void NewItem(int item)
	public void NewItem(Equipment item)
	{
		e.Add (item);
		i = e.Count - 1;
		Debug.Log (e[i]);
	}

	public void UseItem()
	{
		e[i].Use(hand);
        //Equipment ep = e[i];
        e.RemoveAt(i);
        //Destroy(ep.gameObject);
        NextItem();
    }

	private void NextItem()
	{
		i++;
		if (i >= e.Count)
			i = 0;
		Debug.Log (e [i]);
	}

	private void PrevItem()
	{
		i--;
		if (i < 0)
			i = e.Count-1;
		Debug.Log (e [i]);
	}

	public void AddGem (int value)
	{
		if(value>=0)
		collectedGemsValue += value;
	}

	public void AddGold (int value)
	{
		if(value>=0)
		collectedGoldValue += value;
	}

	public void AddOtherValuable (int value)
	{
		if(value>=0)
		collectedOtherValue += value;
	}
}
