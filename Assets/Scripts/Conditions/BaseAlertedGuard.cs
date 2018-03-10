using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		//creature.Targets[creature.Targets.Count-1] = creature.player.position;
		//agent.SetDestination (creature.Targets [creature.Targets.Count - 1]);
		agent.SetDestination(creature.player.position);
			
	}

	public override void SuspicionDecreaseOverTime (Creature creature)
	{
		
	}

	public override void PatrolBehaviour (List<Vector3> spots, ref int index)
	{
		
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
