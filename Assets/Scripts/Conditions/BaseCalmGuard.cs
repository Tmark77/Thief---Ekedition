using System;

namespace AssemblyCSharp
{
	public class BaseCalmGuard : AbstractCondition
	{
		public float NoiseSensitivity; //1 az alap értéke, szorzóként működik

		#region implemented abstract members of AbstractCondition

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
			creature.Suspicion += noiseMeter * NoiseSensitivity;
			if (creature.Suspicion == 100) 
			{
				creature.condition = creature.condition_suspicious;
			}
		}

		public override void ReactToView (Creature creature)
		{
			throw new NotImplementedException ();
		}

		#endregion

		public BaseCalmGuard ()
		{
			carryAble = false;
		}
	}
}

