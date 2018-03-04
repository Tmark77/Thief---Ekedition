using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BaseGuard : Creature
	{
		
		public BaseGuard ()
		{
		}

		public override bool IsInFieldOfView (float angle)
		{
			return Vector3.Distance (player.position, this.transform.position) < 10 && angle < 30f;
		}

		public int damage;
	}
}

