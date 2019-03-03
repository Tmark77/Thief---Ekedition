using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;


public class BaseCalmGuard : AbstractCondition
{
    float waitTime;
	Quaternion startingRotation; //ez jegyzi meg a Lerp-es forgatáshoz, hogy honnan indult (hogy ne lassuljon le forgásközben);
    bool startrotationNULL = true;

    public override void Init(Creature creature)
    {
        creature.walkSpeed.SetToNavMeshAgent(agent);
        waitTime = creature.Targets[creature.index].waitTime;
        agent.SetDestination(creature.Targets[creature.index].position);
    }

    private void Start() //ocsmány törlendő undorító szar. De egyelőre kell.
    {
        Creature c = this.gameObject.GetComponent<Creature>();
        waitTime = c.Targets[0].waitTime;
        agent.SetDestination(c.Targets[c.index].position);
        startrotationNULL = true; //startingRotation = null;
    }

    public override int DamageMultiplier()
    {
        return 3;
    }

    public override void SuspicionDecreaseOverTime(Creature creature)
    {
        //Debug.Log (creature.Suspicion.ToString());
    }

    public override void ReactToNoise(Creature creature, int noiseMeter, Vector3 location)
    {
        creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
        if (creature.Suspicion >= 110)
        {
            creature.Condition = creature.condition_suspicious;
            creature.Targets.Add(new PatrolPost(location));
        }
    }

    public override void ReactToView(Creature creature, int H, int C, int F)
    {
        //Debug.Log ("Észrevettem valamit");
        creature.Suspicion += (int)((H + C + F) / 3);
        if (creature.Suspicion >= 110)
        {
            creature.Condition = creature.condition_suspicious;
            creature.Targets.Add(new PatrolPost(creature.player.position));
        }
    }


    public override AbstractCondition ChangeToKnockedOut(Creature creature)
    {
        return creature.condition_knockeddown;
    }

    public override AbstractCondition ChangeToBlind(Creature creature)
    {
        return creature.condition_blind;
    }

    float turn; //az hogy mennyi egyég legyen a slerp, mert enélkül 20 fokot ugyannannyi idő alatt forogna le mint 270-et.
    float allTurn;

    public override void PatrolBehaviour(Creature creature, ref int index)
    {
        //Debug.Log(Vector3.Distance(agent.transform.position, creature.Targets[index].position));
        if (Vector3.Distance(agent.transform.position, creature.Targets[index].position) < 0.6f) //lehet be kéne hozni valami SelectionCircle szerűséget, hogy mindig meg legyen a Creature talpa
        {
            //Debug.Log((creature.transform.parent.eulerAngles.y - creature.Targets[index].rotation));
            //forgatás
            if ((creature.transform.parent.eulerAngles.y - creature.Targets[index].rotation) > 1)
            {
                Debug.Log(startrotationNULL);
                if (startrotationNULL)
                {
                    Debug.Log("K: " + startrotationNULL);
                    startingRotation = creature.transform.parent.rotation;
                    Debug.Log(startingRotation.eulerAngles);
                    startrotationNULL = false;
                    turn = 4 / (creature.transform.parent.eulerAngles.y - creature.Targets[index].rotation);
                    allTurn = turn;
                    //kurvaanyád
                }
                Debug.Log("this");
                Quaternion lookRotation = new Quaternion();
                lookRotation.eulerAngles = new Vector3(0, creature.Targets[index].rotation, 0);
                allTurn += turn;
                creature.transform.parent.rotation = Quaternion.Lerp(startingRotation, lookRotation, allTurn/*agent.angularSpeed*/);
            }
            else
            {
                Debug.Log("Ebbe kéne befutni");
                //várakozás
                if (waitTime > 0)
                {
                    waitTime = waitTime - Time.deltaTime;
                }
                else //új target
                {
                    startrotationNULL = true; //startingRotation = null;
                    Debug.Log("What?");
                    index++;
                    if (index == creature.Targets.Count)
                    {
                        index = 0;
                    }
                    waitTime = creature.Targets[index].waitTime;
                    agent.SetDestination(creature.Targets[index].position);
                }
            }
        }
    }
}


