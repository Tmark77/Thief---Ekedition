using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : ThiefObject {

	public Transform player;
	public Transform head;
	public Transform chest;
	public Transform foot;

	private int vH;
	private int vC;
	private int vF;

	void Start()
	{
		SuspicionDecrease = 1;
		InvokeRepeating ("DecreaseSuspicion", 0f, 1f);
		index = 0;
	}

	void Update()
	{
		condition.PatrolBehaviour (Targets,ref index);
		//----------------------------------------------------------------------------------------
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle (direction, this.transform.forward);
		if (Vector3.Distance(player.position,this.transform.position) < 10 && angle < 30f) 
		{
			if (Vanralatas (head)) 
			{
				if (Vector3.Distance(head.position,this.transform.position) < IsInShadow.GetVH()) {
					vH = IsInShadow.GetVH ();
				}
			} 
			else 
			{
				vH = 0;
			}

			if (Vanralatas (chest)) 
			{
				if (Vector3.Distance(head.position,this.transform.position) < IsInShadow.GetVC()) {
					vC = IsInShadow.GetVC ();
				}
			} 
			else 
			{
				vC = 0;
			}

			if (Vanralatas (foot)) 
			{
				if (Vector3.Distance(head.position,this.transform.position) < IsInShadow.GetVF()) {
					vF = IsInShadow.GetVF ();
				}
			} 
			else 
			{
				vF = 0;
			}

			condition.ReactToView (this,vH,vC,vF);
		}
	}

	bool Vanralatas(Transform obj)
	{
		RaycastHit[] hits = Physics.RaycastAll (this.transform.position, obj.position - this.transform.position, Vector3.Distance(this.transform.position, obj.position));

		List<ThiefObject> tf = new List<ThiefObject> ();
		foreach (RaycastHit hit in hits) 
		{
			if (hit.collider.gameObject.GetComponent<ThiefObject> () != null) 
			{
				tf.Add (hit.collider.gameObject.GetComponent<ThiefObject> ());
			}
		}

		int ind;
		ind = 0;
		if (tf.Count != 0) {
			do {
				if (!tf[ind].material.SeeTrough())
				{
					return false;
				}
				ind++;
			} while(ind < tf.Count);
		}
		return true;
	}


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

	public List<Transform> Targets = new List<Transform> ();
	int index;
	//ezen targetek módosíthatóak, ezekre a célpontokra fog menni az őr, támadni stb.




}
