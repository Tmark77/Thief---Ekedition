using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : ThiefObject {

	public Transform player;
	public Transform head;
	public Transform chest;
	public Transform foot;
	public AudioSource deadSound;
	public float NoiseSensitivity;
	public int SuspicionDecrease;

	private int vH;
	private int vC;
	private int vF;

	protected int RangeOfVision;
	private float ReactTime;

    public List<Collectible> KnownObjects = new List<Collectible>();
	public List<Collectible> e = new List<Collectible>();
	//ezen targetek módosíthatóak, ezekre a célpontokra fog menni az őr, támadni stb.
	public List<Vector3> Targets = new List<Vector3> ();
	int index;

    public AbstractCondition condition;
	public AbstractCondition condition_calm;
	public AbstractCondition condition_suspicious;
	public AbstractCondition condition_alert;
	public AbstractCondition condition_dead;
	public AbstractCondition condition_knockeddown;
	public AbstractCondition condition_blind;
	public AbstractCondition condition_sleep;

    void Start()
	{
		InvokeRepeating ("DecreaseSuspicion", 0f, 1f);
		index = 0;
		ReactTime = 1f;
	}

	void Update()
	{
		
		condition.PatrolBehaviour (this,ref index);
		//----------------------------------------------------------------------------------------
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle (direction, this.transform.forward);
		if (IsInFieldOfView(angle) && ReactTime < 0f) 
		{
			if (ItIsInLineOfSight (head) && (Vector3.Distance(head.position,this.transform.position) < IsInShadow.GetVH())) 
			{
				vH = IsInShadow.GetVH ();
			} 
			else 
			{
				vH = 0;
			}

			if (ItIsInLineOfSight (chest) && (Vector3.Distance(chest.position,this.transform.position) < IsInShadow.GetVC())) 
			{
				vC = IsInShadow.GetVC ();
			} 
			else 
			{
				vC = 0;
			}

			if (ItIsInLineOfSight (foot) && (Vector3.Distance(foot.position,this.transform.position) < IsInShadow.GetVF())) 
			{
				vF = IsInShadow.GetVF ();
			} 
			else 
			{
				vF = 0;
			}
				
			condition.ReactToView (this,vH,vC,vF);
			ReactTime = 0.5f;
		}
		ReactTime -= Time.deltaTime;
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Vector3 Direction = transform.TransformDirection (Vector3.forward);
		Gizmos.DrawRay (this.transform.position, Direction*20);
		Direction = transform.TransformDirection(new Vector3(20f,0,0));
		Gizmos.DrawRay (this.transform.position, Direction);
		Direction = transform.TransformDirection(new Vector3(-20f,0,0));
		Gizmos.DrawRay (this.transform.position, Direction);
	}
		
	bool ItIsInLineOfSight(Transform obj)
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

	public abstract bool IsInFieldOfView (float angle);

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
			if (value < MaxHealth) {
				health = value;
			}
			else
			{
				health = MaxHealth;
			}
		}
	}

	int suspicion;
	public int Suspicion
	{
		get{ return suspicion; }
		set
		{ 
			if (value <= 200) {
				suspicion = value;
			} 
			else
			{
				suspicion = 200;
			}
		}
	}
		
    public void TakeDamage(int damage)
	{
		if(damage > 0)
			Health = Health - (damage * condition.DamageMultiplier());
		if (Health <= 0) 
		{
			if(condition!=condition_dead)
				deadSound.Play();
			condition = condition.ChangeToDead(this);
            
			//Debug.Log ("Meghaltam, segítség!");
		}
		
	}
		
	public void KnockOut()
	{
		condition = condition.ChangeToKnockedOut (this);
	}

	public void Blinding()
	{
		condition = condition.ChangeToBlind (this);
	}

	public bool Carry()
	{
		return (condition.CarryAble ());
	}

	public void GetNoise(int noiseMeter, Vector3 location)
	{
		condition.ReactToNoise (this, noiseMeter, location);
	}

	public void DecreaseSuspicion()
	{
		//Debug.Log ("Suspicion: " + Suspicion);
		if (Suspicion > 0) {
			Suspicion -= (int)SuspicionDecrease;
			condition.SuspicionDecreaseOverTime (this);
		} 
		else 
		{
			Suspicion = 0;
		}
	}
		





}
