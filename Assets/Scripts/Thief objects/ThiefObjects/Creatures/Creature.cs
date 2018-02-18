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

	public float NoiseSensitivity;

	public int SuspicionDecrease;

	int maxhealth;
	public int MaxHealth
	{
		get{ return maxhealth; }
		set{ maxhealth = value; }
	}

	int health;
	public int Health
	{
		get{ return health; }
		set
		{
			if (value > MaxHealth) {
				health = value;
			}
			else
			{
				health = MaxHealth;
			}
		}
	}

	public AbstractCondition condition;

	public List<Equipment> e = new List<Equipment>();

	int suspicion;
	public int Suspicion
	{
		get{ return suspicion; }
		set
		{ 
			suspicion = value;
		}
	}

	public List<Collectible> KnownObjects = new List<Collectible> ();

	public void TakeDamage(int damage)
	{
		if(damage > 0)
			Health = Health - (damage * condition.DamageMultiplier);
		if (Health <= 0)
			condition = condition_dead;
	}
		
	//na lehet hogy a váltást nem így kéne megcsinálni, hanem a konkrét állapotokon belül.
	public void KnockOut()
	{
		condition = condition.ChangeToKnockedOut (this);
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

	void Update()
	{
		
	}

	void Start()
	{
		SuspicionDecrease = 1;
		InvokeRepeating ("DecreaseSuspicion", 0f, 1f);
	}

	public void DecreaseSuspicion()
	{
		if (Suspicion > 0) {
			Suspicion -= (int)SuspicionDecrease;
			condition.SuspicionDecreaseOverTime (this);
		} 
		else 
		{
			Suspicion = 0;
		}
	}

	List<Transform> Targets = new List<Transform> ();
	//ezen targetek módosíthatóak, ezekre a célpontokra fog menni az őr, támadni stb.




}
