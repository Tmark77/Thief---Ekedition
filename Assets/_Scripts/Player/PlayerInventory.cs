using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public GameObject hand;

	int collectedGemsValue;
	int collectedGoldValue;
	int collectedOtherValue;

    public static int GEMS;
    public static int GOLDS;
    public static int Others;

    public Text goldValue;
	public Text gemValue;
	public Text otherValue;
	public Text equipped_item;
    
    List<Equipment> e = new List<Equipment>();
    int i;

    
    Dictionary<int, int> eq = new Dictionary<int, int>();
    int eqKey;
    

    
	void Start () {
		collectedGemsValue = 0;
		collectedGoldValue = 0;
		collectedOtherValue = 0;
		i = 0;

        eqKey = 0;

        
	}
	
	void Update ()
    {
            if (Input.GetButtonDown("NextItem") || Input.GetAxis("ChangeItem") > 0)
            {
                NextItem();
            }
            if (Input.GetButtonDown("PrevItem") || Input.GetAxis("ChangeItem") < 0)
            {
                PrevItem();
            }

            ManageTexts();

            GEMS = collectedGemsValue;
            GOLDS = collectedGoldValue;
            Others = collectedOtherValue;
    }

	public void ManageTexts()
	{
		if (e.Count != 0)
			equipped_item.text = e [i].Nev + ": " + eq [e [i].Kod];
		else
			equipped_item.text = string.Empty;

		goldValue.text = "Gold: " + collectedGoldValue;

		gemValue.text = "Gem: " + collectedGemsValue;

		otherValue.text = "Other Goods: " + collectedOtherValue;
	}

	
	public void NewItem(Equipment item)
	{
        e.Add(item);
        i = e.Count - 1;
        eqKey = e[i].Kod;
        if (eq.ContainsKey(item.Kod) && !(item is Key))
        {
            eq[item.Kod] += 1;
        }
        else
        {
            eq.Add(item.Kod, 1);
        }
    }

	public void UseItem()
	{
        eqKey = e[i].Kod;

        if(e[i].Use(hand))
        {
			if (eq [eqKey] == 1) {
				eq.Remove (eqKey);
				e.RemoveAt (i);
				if (i == e.Count) 
				{
					NextItem ();
				}
            }
            else
     			       {
				eq[eqKey] --;
                e.RemoveAt(i);
                FindNextSameEquipment();
            }
        }
    }

    private void FindNextSameEquipment()
    {
        for (int j = 0; j < e.Count; j++)
        {
            if (e[j].Kod == eqKey)
            {
                i = j;
                break;
            }
        }
        Debug.Log(e[i] +" " + e[i].Kod + " " + eq[e[i].Kod]);
        
    }

	private void NextItem()
	{
        if (eq.Count>0)
        {
            int temp = eqKey;
            bool found = true;
            foreach (var e in eq)
            {
                if (found)
                {
                    temp = e.Key;
                    found = false;
                }
                if (e.Key == eqKey)
                {
                    found = true;
                }
            }
            eqKey = temp;
            FindNextSameEquipment();
        }
        
    }

	private void PrevItem()
	{
        if (eq.Count > 0)
        {
            int temp = eqKey;
            bool found = false;
            foreach (var e in eq)
            {
                if (e.Key == eqKey && temp != eqKey)
                {
                    found = true;
                }
                if (!found)
                {
                    temp = e.Key;
                }
            }
            eqKey = temp;
            FindNextSameEquipment();
        }
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
