﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockedOutCondition : AbstractCondition {
	#region implemented abstract members of AbstractCondition

	public override void PatrolBehaviour (List<Transform> spots,ref int index)
	{
		throw new System.NotImplementedException ();
	}

	#endregion

	#region implemented abstract members of AbstractCondition

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


	//mily meglepő, ez sincs kész
}
