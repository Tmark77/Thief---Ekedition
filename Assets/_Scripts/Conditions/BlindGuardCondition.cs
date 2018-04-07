using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BlindGuardCondition : AbstractCondition {
	
	public override AbstractCondition ChangeToKnockedOut (Creature creature)
	{
		return creature.condition = creature.condition_knockeddown;
	}

	public override AbstractCondition ChangeToBlind (Creature creature)
	{
		return this;
	}

	public override void ReactToNoise (Creature creature, int noiseMeter, Vector3 location)
	{
		creature.Targets.RemoveAt(creature.Targets.Count - 1);
		creature.Targets.Add(location);
		creature.Suspicion += (int)(noiseMeter * creature.NoiseSensitivity);
	}

	public override void ReactToView (Creature creature, int H, int C, int F)
	{
		
	}

	public override void SuspicionDecreaseOverTime (Creature creature)
	{
		
	}

	public override void PatrolBehaviour (Creature creature, ref int index)
	{
		if (counter == 10f) 
		{
			this.gameObject.GetComponent<Rigidbody> ().isKinematic = true;
			creature.Targets.Add(this.gameObject.transform.position);
		}

		if (counter < 0f) {
			creature.Suspicion = 130;
			counter = 10f;
			creature.condition = creature.condition_suspicious;
		} 

		agent.SetDestination (creature.gameObject.transform.position);
		counter -= Time.deltaTime;
		Debug.Log (counter);
	}

	public override int DamageMultiplier ()
	{
		return 2;
	}

	float counter;

	// Use this for initialization
	void Start () {
		counter = 10f;
		this.agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
