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
	//List<Equipment> e = new List<Equipment>();
    Dictionary<Equipment, int> equipment = new Dictionary<Equipment, int>();
	//int i;

    Equipment eKey;
    int eValue;


	// Use this for initialization
	void Start () {
		collectedGemsValue = 0;
		collectedGoldValue = 0;
		collectedOtherValue = 0;
		//i = 0;

        eKey = null;
        eValue = 0;
		
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
        eKey = item;

        eValue = 1;
        if (equipment.ContainsKey(eKey))
        {
            
            eValue = equipment[eKey] + 1;
            Debug.Log(eValue  + " bennevan ");
            equipment[eKey] = eValue;
        }
        else
        {
            equipment.Add(eKey, eValue);
        }
        

        Debug.Log(eKey + " " + eValue);



        //      e.Add (item);
        //i = e.Count - 1;
        //Debug.Log (e[i]);
    }

	public void UseItem()
	{
        eKey.Use(hand);
        if (eValue <= 1)
        {
            equipment.Remove(eKey);
            NextItem();
        }
        else
        {
            eValue -= 1;
            equipment[eKey] = eValue;
        }


		//e[i].Use(hand);
  //      //Equipment ep = e[i];
  //      e.RemoveAt(i);
  //      //Destroy(ep.gameObject);
  //      NextItem();
    }

	private void NextItem()
	{
        if (equipment.Count > 1)
        {
            Equipment k = eKey;
            int v = eValue;
            bool found = true;
            foreach (var e in equipment)
            {
                if (found)
                {
                    k = e.Key;
                    v = e.Value;
                    found = false;
                }
                if (e.Key == eKey)
                {
                    found = true;
                }
                //Debug.Log("{0}, {1}", pair.Key, pair.Value);
            }
            eKey = k;
            eValue = v;
        }
        Debug.Log(eKey + " " + eValue);

        //i++;
        //if (i >= e.Count)
        //	i = 0;
        //Debug.Log (e [i]);
    }

	private void PrevItem()
	{
        int count = equipment.Count;
        if (count > 1)
        {
            Equipment k = eKey;
            int v = eValue;
            bool found = false;
            foreach (var e in equipment)
            {
                if (e.Key == eKey)
                {
                    found = true;
                }
                if (!found)
                {
                    k = e.Key;
                    v = e.Value;
                }
                //Debug.Log("{0}, {1}", pair.Key, pair.Value);
            }
            eKey = k;
            eValue = v;
        }
        Debug.Log(eKey + " " + eValue);
        //i--;
        //if (i < 0)
        //	i = e.Count-1;
        //Debug.Log (e [i]);
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
