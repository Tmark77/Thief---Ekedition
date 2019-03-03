using System;
using UnityEngine;
using UnityEngine.AI;


public class BaseSuspiciousGuard : AbstractCondition
{
    System.Random rnd = new System.Random();


    public override void Init(Creature creature)
    {
        creature.walkSpeed.SetToNavMeshAgent(agent);
    }

    public override void PatrolBehaviour(Creature creature, ref int index)
    {
        if (Vector3.Distance(agent.transform.position, creature.player.position) <= 3f)
        {
            creature.Targets[creature.Targets.Count - 1] = new PatrolPost(creature.player.position);
            creature.Condition = creature.condition_alert;
        }
        else
        {

            if (agent.destination != creature.Targets[creature.Targets.Count - 1].position)
                agent.SetDestination(creature.Targets[creature.Targets.Count - 1].position);

            if (Vector3.Distance(agent.transform.position, creature.Targets[creature.Targets.Count - 1].position) < 0.5f)
            {
                Vector3 newRandomTarget;
                do
                {
                    newRandomTarget = new Vector3(creature.Targets[creature.Targets.Count - 1].position.x + rnd.Next(-3, 3) * 2, creature.Targets[creature.Targets.Count - 1].position.y, creature.Targets[creature.Targets.Count - 1].position.z + rnd.Next(-3, 3) * 2);
                } while (!agent.CalculatePath(newRandomTarget, new NavMeshPath()));
                creature.Targets[creature.Targets.Count - 1] = new PatrolPost(newRandomTarget);
                //Debug.Log (spots [spots.Count - 1].ToString ());
            }
        }
    }

    public override void ReactToNoise(Creature creature, int noiseMeter, Vector3 location)
    {
        creature.Targets.RemoveAt(creature.Targets.Count - 1);
        creature.Targets.Add(new PatrolPost(location));
        creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
    }

    public override void ReactToView(Creature creature, int H, int C, int F)
    {
        if ((H > 0 || C > 0 || F > 0))
        {
            //Debug.Log ("Látlak");
            creature.Targets[creature.Targets.Count - 1] = new PatrolPost(creature.player.position);
            creature.Suspicion += 30;
            creature.Condition = creature.condition_alert;
        }
    }

    public override void SuspicionDecreaseOverTime(Creature creature)
    {
        //Debug.Log ("csökken a gyanú pont: " + creature.Suspicion);
        if (creature.Suspicion < 100)
        {
            creature.Condition = creature.condition_calm;
            creature.Targets.RemoveAt(creature.Targets.Count - 1);
        }
    }

    //public BaseSuspiciousGuard(NavMeshAgent agent) : base(agent)
    //{

    //}

    //public void Start()
    //{
    //    base.Start();
    //}

    public override AbstractCondition ChangeToBlind(Creature creature)
    {
        creature.Targets.RemoveAt(creature.Targets.Count - 1);
        return creature.condition_blind;
    }

    public override AbstractCondition ChangeToKnockedOut(Creature creature)
    {
        return this;
    }

    
}


