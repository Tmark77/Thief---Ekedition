using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCondition : MonoBehaviour {

	private bool canBeKnockedOut; //leüthető avagy elkábítható-e az adott állapotban a lény.
	public bool CanBeKnockedOut {
		get { return canBeKnockedOut;}
	}

	private int takeDamageMultiplier; //gyanútlan ellenfél nagyobb sebzést kaphat, alap értéke 1
	public int DamageMultiplier {
		get
		{
			return takeDamageMultiplier;
		}
	}

	private bool carryAble; //jobb klikkel viheti a játékos a vállán
	public bool CarryAble {
		get { return carryAble;}
	}

	//nincs kész





}
