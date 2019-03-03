using System;
using UnityEngine;
using UnityEngine.AI;


public class DeadCondition : AbstractCondition
{
    public override void Init(Creature creature)
    {
        creature.gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    public override void PatrolBehaviour(Creature creature, ref int index)
    {
        if (agent.destination != creature.gameObject.transform.position)
            agent.SetDestination(creature.gameObject.transform.position);
    }

    public override AbstractCondition ChangeToKnockedOut(Creature creature)
    {
        return this;
    }

    public override AbstractCondition ChangeToBlind(Creature creature)
    {
        return this;
    }

    public override void SuspicionDecreaseOverTime(Creature creature)
    {
    }

    public override void ReactToNoise(Creature creature, int noiseMeter, Vector3 location)
    {
    }

    public override void ReactToView(Creature creature, int H, int C, int F)
    {
    }

    public override bool CarryAble()
    {
        return true;
    }

    public override bool CanUseThings()
    {
        return false;
    }

}


