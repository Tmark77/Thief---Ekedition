using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
// Ezt majd az egészet át akarom írni a sima őrbe, hogy néhány beállítással lehessem megadni meddig álldogáljon, és merre nézzen közben.
public class PostingClamGuardCondition : AbstractCondition
{

	[SerializeField] public Transform LookAtThis;

	public Material mat;


    public override void Init(Creature creature)
    {
        creature.walkSpeed.SetToNavMeshAgent(agent);
    }

    public void Start()
    {
        mat.color = Color.yellow;
        base.Awake();
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
			creature.Condition = creature.condition_suspicious;
			creature.Targets.Add(new PatrolPost(location));
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
			creature.Condition = creature.condition_suspicious;
			creature.Targets.Add (new PatrolPost(creature.player.position));
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
		if (Vector3.Distance(agent.transform.position,creature.Targets[index].position) < 0.5f ) 
		{
			Vector3 direction = (LookAtThis.position - creature.transform.parent.position).normalized;
			Quaternion lookRotation = Quaternion.LookRotation(direction);
            creature.transform.parent.rotation = Quaternion.Slerp(creature.transform.parent.rotation, lookRotation, Time.deltaTime * agent.angularSpeed/100);
			//index++;
		}
		else agent.SetDestination (creature.Targets[index].position);

		if (index == creature.Targets.Count) 
		{
			index = 0;
		}

	}

}
