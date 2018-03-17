using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AbstractCondition : MonoBehaviour {

	//private bool canBeKnockedOut; //leüthető avagy elkábítható-e az adott állapotban a lény.
	//public bool CanBeKnockedOut {
	//	get { return canBeKnockedOut;}
	//}

	public virtual int DamageMultiplier () //gyanútlan ellenfél nagyobb sebzést kaphat, alap értéke 1
	{
		return 1; //1 az alap értéke, szorzóként működik
	}

    protected NavMeshAgent agent;

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

	abstract public void ReactToNoise (Creature creature, int noiseMeter);

	abstract public void ReactToView (Creature creature,int H, int C, int F);

	public abstract void SuspicionDecreaseOverTime (Creature creature);

	//kell hogy egy célpont felé hogyan reagál


	public abstract void PatrolBehaviour (Creature creature,ref int index);


}
