using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractCondition : MonoBehaviour {

	//private bool canBeKnockedOut; //leüthető avagy elkábítható-e az adott állapotban a lény.
	//public bool CanBeKnockedOut {
	//	get { return canBeKnockedOut;}
	//}

	private int takeDamageMultiplier; //gyanútlan ellenfél nagyobb sebzést kaphat, alap értéke 1

	public int DamageMultiplier {
		get
		{
			return takeDamageMultiplier;
		}
	}

	protected bool carryAble; //jobb klikkel viheti a játékos a vállán
	public bool CarryAble {
		get { return carryAble;}
	}
		
	public AbstractCondition ChangeToDead(Creature creature)
	{
		return creature.condition_dead;
	}

	//--------------------------------------------------
	public abstract AbstractCondition ChangeToKnockedOut (Creature creature);

	public abstract AbstractCondition ChangeToBlind (Creature creature);

	//-----------------------------------------------------
	public virtual AbstractCondition ChangeToSuspicious ()
	{
		return this;
	}

	public virtual AbstractCondition ChangeToAlerted ()
	{
		return this;
	}

	public virtual AbstractCondition ChangeToCalm ()
	{
		return this;
	}
		
	//---------------------------------------------------
	abstract public void ReactToNoise (Creature creature, int noiseMeter);

	abstract public void ReactToView (Creature creature);

	public abstract void SuspicionDecreaseOverTime (Creature creature);

	//kell hogy egy célpont felé hogyan reagál





}
