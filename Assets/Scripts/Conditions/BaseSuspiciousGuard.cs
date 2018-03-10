using System;
using UnityEngine;
using UnityEngine.AI;

namespace AssemblyCSharp
{
	public class BaseSuspiciousGuard : AbstractCondition
	{
        System.Random rnd = new System.Random();

        #region implemented abstract members of AbstractCondition

        public override void PatrolBehaviour (System.Collections.Generic.List<Vector3> spots, ref int index)
		{
            agent.SetDestination(spots[spots.Count-1]);

            if (Vector3.Distance(agent.transform.position, spots[spots.Count - 1]) < 1f)
            {
                spots[spots.Count - 1] = new Vector3(spots[spots.Count - 1].x+rnd.Next(-3,3), spots[spots.Count - 1].y, spots[spots.Count - 1].z+rnd.Next(-3, 3)); //SZAR MÉG              
            }
        }

		#endregion

		public Material mat;
		#region implemented abstract members of AbstractCondition

		public override void ReactToNoise (Creature creature, int noiseMeter)
		{
            creature.Targets.RemoveAt(creature.Targets.Count - 1);
            creature.Targets.Add(creature.player.position);
			creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
			if (creature.Suspicion >= 200) 
			{
				creature.condition = creature.condition_alert;
			}
		}

		public override void ReactToView (Creature creature,int H, int C, int F)
		{
           
        }

		public override void SuspicionDecreaseOverTime (Creature creature)
		{
			Debug.Log ("csökken a gyanú pont: " + creature.Suspicion);
			if(creature.Suspicion<100)
			{
				mat.color = Color.yellow;
				creature.condition = creature.condition_calm;
                creature.Targets.RemoveAt(creature.Targets.Count - 1);
			}
		}

		#endregion

		void Start ()
		{
			carryAble = false;
            this.agent = GetComponent<NavMeshAgent>();
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

