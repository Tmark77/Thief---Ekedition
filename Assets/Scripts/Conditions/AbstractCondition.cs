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
	public virtual AbstractCondition ChangeToKnockedOut ()
	{
		return this;
	}

	public virtual AbstractCondition ChangeToBlind ()
	{
		return this;
	}
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

	//nincs kész





}
