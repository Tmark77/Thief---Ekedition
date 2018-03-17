﻿using System;
using UnityEngine;

namespace AssemblyCSharp
{
	public class BaseGuard : Creature
	{

		public BaseGuard ()
		{
			RangeOfVision = 20;
			MaxHealth = 100;
			Health = MaxHealth;
		}

		public override bool IsInFieldOfView (float angle)
		{
			return Vector3.Distance (player.position, this.transform.position) < RangeOfVision && angle < 90f;
		}

		public int damage;
	}
}

