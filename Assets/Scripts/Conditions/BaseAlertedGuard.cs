using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseAlertedGuard : AbstractCondition {
	
	public override AbstractCondition ChangeToKnockedOut (Creature creature)
	{
		return this;
	}

	public override AbstractCondition ChangeToBlind (Creature creature)
	{
		return creature.condition_blind;
	}

	public override void ReactToNoise (Creature creature, int noiseMeter)
	{
		
	}

	public override void ReactToView (Creature creature, int H, int C, int F)
	{
		if ((H > 0 || C > 0 || F > 0)) {
			creature.Targets [creature.Targets.Count - 1] = creature.player.position;
			//agent.SetDestination (creature.Targets [creature.Targets.Count - 1]);
			//ez a damage csak unit test
			//marhára nem ide kell rakni, hanem majd a PatrolBehaviour-be, csak ott még elérés pobléma van
			//ja és a damage elérés is poblémás még
			creature.player.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
		}
		else
		{
			creature.condition = creature.condition_suspicious;
		}
			
	}

	public override void SuspicionDecreaseOverTime (Creature creature)
	{
		creature.Suspicion += 2;
	}

	public override void PatrolBehaviour (Creature creature, ref int index)
	{
		agent.destination = creature.Targets[creature.Targets.Count-1];
	}


	// Use this for initialization
	void Start () {
		this.agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
