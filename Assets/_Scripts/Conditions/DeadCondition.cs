using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class DeadCondition : AbstractCondition
	{

		public override void PatrolBehaviour (Creature creature,ref int index)
		{
			
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

		void Start ()
		{
			
		}

		public override void ReactToNoise (Creature creature, int noiseMeter, Vector3 location)
		{
		}

		public override void ReactToView (Creature creature,int H, int C, int F)
		{
		}

		public override bool CarryAble ()
		{
			return true;
		}


	}
}

