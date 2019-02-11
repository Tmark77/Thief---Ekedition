using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SleepCondition : AbstractCondition {
	
	#region implemented abstract members of AbstractCondition
	public override AbstractCondition ChangeToKnockedOut (Creature creature)
	{
		throw new System.NotImplementedException ();
	}
	public override AbstractCondition ChangeToBlind (Creature creature)
	{
		throw new System.NotImplementedException ();
	}
	public override void ReactToNoise (Creature creature, int noiseMeter, Vector3 location)
	{
		throw new System.NotImplementedException ();
	}
	public override void ReactToView (Creature creature, int H, int C, int F)
	{
		throw new System.NotImplementedException ();
	}
	public override void SuspicionDecreaseOverTime (Creature creature)
	{
		throw new System.NotImplementedException ();
	}
	public override void PatrolBehaviour (Creature creature, ref int index)
	{
		throw new System.NotImplementedException ();
	}
    #endregion


    //public SleepCondition(NavMeshAgent agent) : base(agent)
    //{

    //}

    //public void Start()
    //{
    //    base.Start();        
    //}

    public override int DamageMultiplier ()
	{
		return 3;
	}

    public override bool CanUseThings()
    {
        return false;
    }
}
