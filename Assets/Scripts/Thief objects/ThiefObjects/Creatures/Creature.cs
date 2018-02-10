using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : ThiefObject {

	int health;
	public int Health
	{
		get{ return health; }
		set{ health = value;}
	}
		
	public AbstractCondition condition;

	int suspicion;
	public int Suspicion
	{
		get{ return suspicion; }
		set{ suspicion = value;}
	}

	public List<Collectible> KnownObjects = new List<Collectible> ();

	public void TakeDamage(int damage)
	{
		if(damage > 0)
			Health = Health - (damage * condition.DamageMultiplier);
	}
		
	//na lehet hogy a váltást nem így kéne mgecsinálni, hanem a konkrét állapotokon belül.
	public void KnockOut()
	{
		if (condition.CanBeKnockedOut) 
		{
			// + játszd le az összeeső animációt
			condition = new KnockedOutCondition ();
		}
	}

	public bool Carry()
	{
		if (condition.CarryAble) 
		{
			// + játszd le a felvevő animációt
			return true;
		}
		return false;
	}

}
