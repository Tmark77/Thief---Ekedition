using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

namespace AssemblyCSharp
{
	public class BaseCalmGuard : AbstractCondition
	{
		NavMeshAgent agent;

		public Material mat;

		void Start ()
		{
			carryAble = false;
			mat.color = Color.yellow;
			this.agent = GetComponent<NavMeshAgent> ();
		}

		void Update()
		{
			
		}

		//1 az alap értéke, szorzóként működik

		public override void SuspicionDecreaseOverTime (Creature creature)
		{
			
		}

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
			creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
			if (creature.Suspicion >= 100) 
			{
				mat.color = Color.red;
				creature.condition = creature.condition_suspicious;
				agent.SetDestination (creature.gameObject.transform.position);
			}
		}

		public override void ReactToView (Creature creature,int H, int C, int F)
		{
			if (H > 10 || C > 10 || F > 10) 
			{
				Debug.Log ("Látlak");
				creature.Suspicion = 110;
				mat.color = Color.red;
				creature.condition = creature.condition_suspicious;
				agent.SetDestination (creature.player.position);
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
			
		public override void PatrolBehaviour (List<Transform> spots, ref int index)
		{
			if (Vector3.Distance(agent.transform.position,spots[index].position) < 1 ) 
			{
				index++;
			}
			else agent.SetDestination (spots [index].position);

			if (index == spots.Count) {
				index = 0;
			}
		}
	}
}

