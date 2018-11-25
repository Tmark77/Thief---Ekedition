using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace AssemblyCSharp
{
	public class BaseCalmGuard : AbstractCondition
	{
		

		public Material mat;

		void Start ()
		{
			base.Start ();
			mat.color = Color.yellow;
			//agent.areaMask =  NavMesh.GetAreaFromName("DOOR");
			//agent.areaMask = NavMesh.AllAreas;

        }

		void Update()
		{
			
		}

		public override int DamageMultiplier ()
		{
			return 3;
		}

		public override void SuspicionDecreaseOverTime (Creature creature)
		{
			//Debug.Log (creature.Suspicion.ToString());
		}

		public override void ReactToNoise (Creature creature, int noiseMeter, Vector3 location)
		{
			creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
			if (creature.Suspicion >= 110) 
			{
				mat.color = Color.red;
				creature.condition = creature.condition_suspicious;
				creature.Targets.Add(location);
			}
		}

		public override void ReactToView (Creature creature,int H, int C, int F)
		{
			
			//Debug.Log ("Észrevettem valamit");
			creature.Suspicion += (int)((H + C + F) / 3);
			//Debug.Log ("Fasz " +(int)((H + C + F) / 3));
			if (creature.Suspicion >= 110) 
			{
				mat.color = Color.red;	
				creature.condition = creature.condition_suspicious;
				creature.Targets.Add (creature.player.position);
			}

		}


		public override AbstractCondition ChangeToKnockedOut (Creature creature)
		{
			return creature.condition_knockeddown;
		}

		public override AbstractCondition ChangeToBlind (Creature creature)
		{
			return creature.condition_blind;
		}
			
		public override void PatrolBehaviour (Creature creature, ref int index)
		{
			if (Vector3.Distance(agent.transform.position,creature.Targets[index]) < 0.5f ) 
			{
				index++;
				if (index == creature.Targets.Count) {
					index = 0;
				}
			}
            if (agent.destination != creature.Targets[index])
            {
                agent.SetDestination(creature.Targets[index]);
            }


		}
	}
}

