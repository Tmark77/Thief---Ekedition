using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOutCondition : AbstractCondition {
	#region implemented abstract members of AbstractCondition

	public override void PatrolBehaviour (Creature creature,ref int index)
	{
		
		if(counter < 0f)
		{
			creature.Suspicion = 130;
			counter = 10f;
			creature.condition = creature.condition_suspicious;
		}
		counter -= Time.deltaTime;
		Debug.Log (counter);
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

	public override void ReactToNoise (Creature creature, int noiseMeter)
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
	}

	//mily meglepő, ez sincs kész
}
