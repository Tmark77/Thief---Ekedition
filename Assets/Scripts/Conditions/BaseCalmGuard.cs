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
			carryAble = false;
			mat.color = Color.yellow;
            this.agent = GetComponent<NavMeshAgent>();
        }

		void Update()
		{
			
		}

		//1 az alap értéke, szorzóként működik

		public override void SuspicionDecreaseOverTime (Creature creature)
		{
			//Debug.Log (creature.Suspicion.ToString());
		}

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
			creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
			if (creature.Suspicion >= 110) 
			{
				mat.color = Color.red;
				creature.condition = creature.condition_suspicious;
				creature.Targets.Add(creature.player.position);
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
			if (Vector3.Distance(agent.transform.position,creature.Targets[index]) < 1f ) 
			{
				index++;
			}
			else agent.SetDestination (creature.Targets [index]);

			if (index == creature.Targets.Count) {
				index = 0;
			}
		}
	}
}

