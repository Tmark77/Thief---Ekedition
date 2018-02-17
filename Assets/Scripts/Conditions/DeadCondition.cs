using System;

namespace AssemblyCSharp
{
	public class DeadCondition : AbstractCondition
	{
		#region implemented abstract members of AbstractCondition

		public DeadCondition ()
		{
			carryAble = true;
		}

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
		}

		public override void ReactToView (Creature creature)
		{
		}

		#endregion
			
	}
}

