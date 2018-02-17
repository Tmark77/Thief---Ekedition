using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : ThiefObject {

	public AbstractCondition condition_calm;
	public AbstractCondition condition_suspicious;
	public AbstractCondition condition_alert;
	public AbstractCondition condition_dead;
	public AbstractCondition condition_knockeddown;
	public AbstractCondition condition_blind;
	public AbstractCondition condition_sleep;

	int health;
	public int Health
	{
		get{ return health; }
		set{ health = value;}
	}
		
	public AbstractCondition condition;

	public List<Equipment> e = new List<Equipment>();

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
		
	//na lehet hogy a váltást nem így kéne megcsinálni, hanem a konkrét állapotokon belül.
	public void KnockOut()
	{
		condition = condition.ChangeToKnockedOut ();
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

	public void GetNoise(int noiseMeter)
	{
		condition.ReactToNoise (this, noiseMeter);
	}

	//metódus ami a látott dologokat észleli



}
