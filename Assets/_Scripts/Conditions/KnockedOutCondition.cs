using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockedOutCondition : AbstractCondition {
	#region implemented abstract members of AbstractCondition

	public override void PatrolBehaviour (Creature creature,ref int index)
	{
		if (counter == 10f) 
		{
			this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
		}
		
		if (counter < 0f) {
			creature.Suspicion = 130;
			counter = 10f;
			creature.condition = creature.condition_suspicious;
		} 
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
		counter = 10f;
		this.agent = GetComponent<NavMeshAgent>();
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
