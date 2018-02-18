using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BaseCalmGuard : AbstractCondition
	{
		public Material mat;

		//1 az alap értéke, szorzóként működik
		#region implemented abstract members of AbstractCondition

		public override void SuspicionDecreaseOverTime (Creature creature)
		{
			
		}

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
			creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
			Debug.Log ("növekvő gyanú: " + creature.Suspicion);
			if (creature.Suspicion >= 100) 
			{
				mat.color = Color.red;
				creature.condition = creature.condition_suspicious;
			}
		}

		public override void ReactToView (Creature creature)
		{
			throw new NotImplementedException ();
		}

		#endregion

		void Start ()
		{
			carryAble = false;
			mat.color = Color.yellow;
		}

		public override AbstractCondition ChangeToKnockedOut (Creature creature)
		{
			return creature.condition_knockeddown;
		}

		public override AbstractCondition ChangeToBlind (Creature creature)
		{
			return creature.condition_blind;
		}
			
			
	}
}

