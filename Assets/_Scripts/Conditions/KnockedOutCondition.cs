using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KnockedOutCondition : AbstractCondition
{
	
	float counter;

    public override void Init(Creature creature)
    {
        counter = 60f;
        creature.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void PatrolBehaviour (Creature creature,ref int index)
    {
		if (counter < 0f) {
			creature.Suspicion = 130;
			creature.Targets.Add (new PatrolPost(creature.gameObject.transform.position));
			creature.Condition = creature.condition_suspicious;
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

    public override int DamageMultiplier ()
	{
		return 3;
	}

	public override bool CarryAble ()
	{
		return true;
	}

   
}
