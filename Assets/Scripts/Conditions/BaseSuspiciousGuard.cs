using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BaseSuspiciousGuard : AbstractCondition
	{
		#region implemented abstract members of AbstractCondition

		public override void PatrolBehaviour (System.Collections.Generic.List<Transform> spots, ref int index)
		{
			
		}

		#endregion

		public Material mat;
		#region implemented abstract members of AbstractCondition

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
			creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
			if (creature.Suspicion >= 200) 
			{
				creature.condition = creature.condition_alert;
			}
		}

		public override void ReactToView (Creature creature,int H, int C, int F)
		{
			throw new NotImplementedException ();
		}

		public override void SuspicionDecreaseOverTime (Creature creature)
		{
			//Debug.Log ("csökken a gyanú pont: " + creature.Suspicion);
			if(creature.Suspicion<100)
			{
				mat.color = Color.yellow;
				creature.condition = creature.condition_calm;
			}
		}

		#endregion

		void Start ()
		{
			carryAble = false;
		}

		public override AbstractCondition ChangeToBlind (Creature creature)
		{
			return creature.condition_blind;
		}

		public override AbstractCondition ChangeToKnockedOut (Creature creature)
		{
			return this;
		}
			
	

	}
}

