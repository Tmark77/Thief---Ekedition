using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AssemblyCSharp;

public class BaseAlertedGuard : AbstractCondition
{

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
            creature.Targets[creature.Targets.Count - 1] = creature.player.position;
            //agent.SetDestination (creature.Targets [creature.Targets.Count - 1]);
        }
        else
        {
            if (Vector3.Distance(agent.transform.position, creature.player.position) > 2f)
            {
                creature.condition = creature.condition_suspicious;
            }

        }

    }

    public override void SuspicionDecreaseOverTime(Creature creature)
    {
        creature.Suspicion += 2;
    }

    private float hitCooldown = 0f;

    public override void PatrolBehaviour(Creature creature, ref int index)
    {
        if (Vector3.Distance(agent.transform.position, creature.player.position) <= 3f)
        {
            agent.SetDestination(this.gameObject.transform.position);
            Vector3 direction = (creature.player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * agent.angularSpeed);
            if (hitCooldown < 0f)
            {
                hitCooldown = 2f;
				Debug.Log ("fasz");
				creature.player.gameObject.GetComponent<PlayerHealth>().TakeDamage((creature as BaseGuard).damage);
            }
        }
        else
        {
            agent.destination = creature.Targets[creature.Targets.Count - 1];
            creature.condition = creature.condition_suspicious;
        }
        hitCooldown -= Time.deltaTime;

    }


    // Use this for initialization
    void Start()
    {
        this.agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
