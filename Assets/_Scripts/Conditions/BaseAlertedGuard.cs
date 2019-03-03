using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAlertedGuard : AbstractCondition
{
    private float hitCooldown = 0f;

    public override void Init(Creature creature)
    {
        creature.runSpeed.SetToNavMeshAgent(agent);
        //hitCooldown = 0f;
    }

    public override AbstractCondition ChangeToKnockedOut(Creature creature)
    {
        return this;
    }

    public override AbstractCondition ChangeToBlind(Creature creature)
    {
		creature.Targets.RemoveAt(creature.Targets.Count - 1);
        return creature.condition_blind;
    }

	public override void ReactToNoise(Creature creature, int noiseMeter, Vector3 location)
    {

    }

    public override void ReactToView(Creature creature, int H, int C, int F)
    {
        if ((H > 0 || C > 0 || F > 0))
        {
            creature.Targets[creature.Targets.Count - 1].position = creature.player.position;
        }
        else
        {
            if (Vector3.Distance(agent.transform.position, creature.player.position) > 2f)
            {
                creature.Condition = creature.condition_suspicious;
            }

        }

    }

    public override void SuspicionDecreaseOverTime(Creature creature)
    {
		creature.Suspicion += 2 * creature.SuspicionDecrease;
    }

    public override void PatrolBehaviour(Creature creature, ref int index)
    {
        if (Vector3.Distance(agent.transform.position, creature.player.position) <= 3f)
        {
            if (agent.destination != creature.gameObject.transform.position)
            {
                agent.SetDestination(creature.gameObject.transform.position);
            }
			Vector3 direction = (creature.player.position - creature.transform.parent.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            creature.transform.parent.rotation = Quaternion.Slerp(creature.transform.parent.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
            if (hitCooldown < 0f)
            {
                hitCooldown = 2f;
				creature.player.gameObject.GetComponent<PlayerHealth>().TakeDamage((creature as BaseGuard).damage);
            }
        }
        else
        {
            if(agent.destination != creature.Targets[creature.Targets.Count - 1].position)
            agent.SetDestination(creature.Targets[creature.Targets.Count - 1].position);
        }
        hitCooldown -= Time.deltaTime;

    }
}
