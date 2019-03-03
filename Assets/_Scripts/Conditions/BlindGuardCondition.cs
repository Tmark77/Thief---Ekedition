using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlindGuardCondition : AbstractCondition
{
	float counter;

    public override void Init(Creature creature)
    {
        counter = 10f;
        creature.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        creature.Targets.Add(new PatrolPost(creature.gameObject.transform.position));
    }

    public override AbstractCondition ChangeToKnockedOut (Creature creature)
	{
		return creature.Condition = creature.condition_knockeddown;
	}

	public override AbstractCondition ChangeToBlind (Creature creature)
	{
		return this;
	}

	public override void ReactToNoise (Creature creature, int noiseMeter, Vector3 location)
	{
		creature.Targets.RemoveAt(creature.Targets.Count - 1);
		creature.Targets.Add(new PatrolPost(location));
		creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
	}

	public override void ReactToView (Creature creature, int H, int C, int F)
	{
		
	}

	public override void SuspicionDecreaseOverTime (Creature creature)
	{
		
	}

	public override void PatrolBehaviour (Creature creature, ref int index)
	{
		if (counter < 0f)
        {
			creature.Suspicion = 130;
			creature.Condition = creature.condition_suspicious;
		}
        if (agent.destination != creature.gameObject.transform.position)
            agent.SetDestination (creature.gameObject.transform.position);

		counter -= Time.deltaTime;
		Debug.Log (counter);
	}

	public override int DamageMultiplier ()
	{
		return 2;
	}

    public override bool CanUseThings()
    {
        return false;
    }
}
