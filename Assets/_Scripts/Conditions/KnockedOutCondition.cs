using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockedOutCondition : AbstractCondition {
	#region implemented abstract members of AbstractCondition

	public override void PatrolBehaviour (Creature creature,ref int index)
	{
		if (counter == 60f) 
		{
			this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		}
		
		if (counter < 0f) {
			creature.Suspicion = 130;
			counter = 60f;
			creature.Targets.Add (this.gameObject.transform.position);
			creature.condition = creature.condition_suspicious;
		}
        if (agent.destination != creature.gameObject.transform.position)
            agent.SetDestination (creature.gameObject.transform.position);
		counter -= Time.deltaTime;
		//Debug.Log (counter);
	}
		
	public override AbstractCondition ChangeToKnockedOut (Creature creature)
	{
		return this;
	}

	public override AbstractCondition ChangeToBlind (Creature creature)
	{
		return this;
	}

	public override void SuspicionDecreaseOverTime (Creature creature)
	{
	}

	public override void ReactToNoise (Creature creature, int noiseMeter, Vector3 location)
	{
	}

	public override void ReactToView (Creature creature,int H, int C, int F)
	{
	}
		
	#endregion
	float counter;

	void Start()
	{
		counter = 60f;
		base.Start ();
	}

	public override int DamageMultiplier ()
	{
		return 3;
	}

	public override bool CarryAble ()
	{
		return true;
	}


}
