using System;
using UnityEngine;
using UnityEngine.AI;

namespace AssemblyCSharp
{
    public class BaseSuspiciousGuard : AbstractCondition
    {
        System.Random rnd = new System.Random();

        #region implemented abstract members of AbstractCondition

        public override void PatrolBehaviour(Creature creature, ref int index)
        {
            if (Vector3.Distance(agent.transform.position, creature.player.position) <= 2f)
            {
                creature.Targets[creature.Targets.Count - 1] = creature.player.position;
                creature.condition = creature.condition_alert;
            }
            else
            {
                agent.SetDestination(creature.Targets[creature.Targets.Count - 1]);

                if (Vector3.Distance(agent.transform.position, creature.Targets[creature.Targets.Count - 1]) < 0.5f)
                {
                    Vector3 fasz; //át kell majd írni
                    do
                    {
                        fasz = new Vector3(creature.Targets[creature.Targets.Count - 1].x + rnd.Next(-3, 3) * 2, creature.Targets[creature.Targets.Count - 1].y, creature.Targets[creature.Targets.Count - 1].z + rnd.Next(-3, 3) * 2);
                        //SZAR MÉG   
                    } while (!agent.CalculatePath(fasz, new NavMeshPath()));
                    creature.Targets[creature.Targets.Count - 1] = fasz;
                    //Debug.Log (spots [spots.Count - 1].ToString ());
                }
            }
        }

        #endregion

        public Material mat;
        #region implemented abstract members of AbstractCondition

        public override void ReactToNoise(Creature creature, int noiseMeter)
        {
            creature.Targets.RemoveAt(creature.Targets.Count - 1);
            creature.Targets.Add(creature.player.position);
            creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
            //if (creature.Suspicion >= 200) 
            //{
            //	creature.condition = creature.condition_alert;
            //}
        }

        public override void ReactToView(Creature creature, int H, int C, int F)
        {
            if ((H > 0 || C > 0 || F > 0))
            {
                //Debug.Log ("Látlak");
                //creature.Suspicion = 210;
                //mat.color = Color.blue;
                creature.Targets.Add(creature.player.position);
                creature.Suspicion += 30;
                creature.condition = creature.condition_alert;
            }
        }

        public override void SuspicionDecreaseOverTime(Creature creature)
        {
            //Debug.Log ("csökken a gyanú pont: " + creature.Suspicion);
            if (creature.Suspicion < 100)
            {
                mat.color = Color.yellow;
                creature.condition = creature.condition_calm;
                creature.Targets.RemoveAt(creature.Targets.Count - 1);
            }
        }

        #endregion

        void Start()
        {
            carryAble = false;
            this.agent = GetComponent<NavMeshAgent>();
        }

        public override AbstractCondition ChangeToBlind(Creature creature)
        {
            return creature.condition_blind;
        }

        public override AbstractCondition ChangeToKnockedOut(Creature creature)
        {
            return this;
        }



    }
}

