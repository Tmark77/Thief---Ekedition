using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class AbstractCondition : MonoBehaviour {

	//private bool canBeKnockedOut; //leüthető avagy elkábítható-e az adott állapotban a lény.
	//public bool CanBeKnockedOut {
	//	get { return canBeKnockedOut;}
	//}

	public void Start()
	{
		this.agent = GetComponentInParent<NavMeshAgent>();
	}

	protected NavMeshAgent agent;

	public virtual int DamageMultiplier () //gyanútlan ellenfél nagyobb sebzést kaphat, alap értéke 1
	{
		return 1; //1 az alap értéke, szorzóként működik
	}
		
	public virtual bool CarryAble()
	{
		return false;
	}
		
	public AbstractCondition ChangeToDead(Creature creature)
	{
		this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		return creature.condition_dead;
	}

	//--------------------------------------------------
	public abstract AbstractCondition ChangeToKnockedOut (Creature creature);

	public abstract AbstractCondition ChangeToBlind (Creature creature);

	public abstract void SuspicionDecreaseOverTime (Creature creature);

	//kell hogy egy célpont felé hogyan reagál
	public abstract void PatrolBehaviour (Creature creature,ref int index);

	public abstract void ReactToNoise (Creature creature, int noiseMeter, Vector3 location);

	public abstract void ReactToView (Creature creature,int H, int C, int F);

    public virtual bool CanUseThings()
    {
        return true;
    }


	public void DoorAvaible (int doorID)
	{
		this.Start ();
		if (CanUseThings() && ((agent.areaMask & (int)Math.Pow (2, doorID)) != (int)Math.Pow(2, doorID)))
		{
			agent.areaMask = agent.areaMask + (int)Math.Pow (2, doorID);
			Debug.Log ("avaible: "+ doorID);
		}

	}

	//az útvonaltervből kiveszi az ajtót, theát nem mehet át az adott ajtón az őr
	public void DoorDisable (int doorID)
	{
		if ((agent.areaMask & (int)Math.Pow (2, doorID)) == (int)Math.Pow(2, doorID))
		{
			agent.areaMask = agent.areaMask - (int)Math.Pow(2, doorID);
			Debug.Log ("denied: "+ doorID);
		}

	}



	//-----------------------------------------------------



}
