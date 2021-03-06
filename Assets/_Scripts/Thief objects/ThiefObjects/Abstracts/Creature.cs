﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : ThiefObject, I_Highlightable
{
	[System.NonSerialized] public Transform player;
	[System.NonSerialized] public Transform head;
	[System.NonSerialized] public Transform chest;
	[System.NonSerialized] public Transform foot;
	public AudioSource deadSound;
	public float NoiseSensitivity;
	public int SuspicionDecrease;

	private int vH;
	private int vC;
	private int vF;

	[SerializeField][Range(0,100)] public int RangeOfVision; //protectedbe jobb lenne, lehet írok hozzá propertyt meg propertydrawert
	private float ReactTime;

    public List<Collectible> KnownObjects = new List<Collectible>();
	[System.NonSerialized] public List<Collectible> e = new List<Collectible>();
	//ezen targetek módosíthatóak, ezekre a célpontokra fog menni az őr, támadni stb.
	public List<PatrolPost> Targets = new List<PatrolPost> ();
    [HideInInspector]
	public int index; //public, hogy rohadjon meg

    /*[HideInInspector] */public AbstractCondition condition; //nem kéne h publik legyen gec
    [HideInInspector] public AbstractCondition Condition
    {
        get { return condition; }
        set
        {
            condition = value;
            condition.Init(this);
        }
    }

    /*[HideInInspector]*/ public AbstractCondition condition_calm;
	/*[HideInInspector]*/ public AbstractCondition condition_suspicious;
	/*[HideInInspector]*/ public AbstractCondition condition_alert;
	/*[HideInInspector]*/ public AbstractCondition condition_dead;
	/*[HideInInspector]*/ public AbstractCondition condition_knockeddown;
	/*[HideInInspector]*/ public AbstractCondition condition_blind;
	/*[HideInInspector]*/ public AbstractCondition condition_sleep;

    //értéket kell nekik adni a gyerek osztályokban!
    public Speed walkSpeed;
    public Speed runSpeed;


    void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		head = player.Find ("Head").transform;
		chest = player.Find ("Chest").transform;
		foot = player.Find ("Foot").transform;
		InvokeRepeating ("DecreaseSuspicion", 0f, 1f);
		index = 0;
		ReactTime = 1f;
        e.AddRange(this.gameObject.GetComponentsInChildren<Collectible>());
        for (int i = 0; i < e.Count; i++) 
		{
            if (e [i] is Equipment && ((e [i] as Equipment).Kod >= 10 && (e [i] as Equipment).Kod <= 20)) 
			{
                Condition.DoorAvaible ((e [i] as Equipment).Kod);
			}
		}

	}

	void Update()
	{
		Condition.PatrolBehaviour (this,ref index);
		//----------------------------------------------------------------------------------------
		if(ReactTime < 0f)
		{
			ReactTime = 0.5f;
			Vector3 direction = player.position - this.transform.position;
			float angle = Vector3.Angle (direction, this.transform.forward);
			if (IsInFieldOfView(angle)) 
			{
				if (IsInLineOfSight (head) && (Vector3.Distance(head.position,this.transform.position) < IsInShadow.GetVH())) 
				{
					vH = IsInShadow.GetVH ();
				} 
				else 
				{
					vH = 0;
				}
			
				if (IsInLineOfSight (chest) && (Vector3.Distance(chest.position,this.transform.position) < IsInShadow.GetVC())) 
				{
					vC = IsInShadow.GetVC ();
				} 
				else 
				{
					vC = 0;
				}
			
				if (IsInLineOfSight (foot) && (Vector3.Distance(foot.position,this.transform.position) < IsInShadow.GetVF())) 
				{
					vF = IsInShadow.GetVF ();
				} 
				else 
				{
					vF = 0;
				}
					
				Condition.ReactToView (this,vH,vC,vF);
			}
		}
		ReactTime -= Time.deltaTime;
	}

	#region I_Highlightable implementation
	public void Highlight()
	{
        if (Condition.CarryAble())
        {
            if (this.gameObject.GetComponent<Renderer>() != null)
            {
                this.gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
                this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(0.3f, 0.3f, 0.3f));
            }
            Renderer[] rend = this.gameObject.transform.GetComponentsInChildren<Renderer>();
            foreach (Renderer item in rend)
            {
                item.material.SetColor("_EmissionColor", new Color(0.3f, 0.3f, 0.3f));
            }
        }
	}

	public void DeHighlight()
	{
        if (this.gameObject.GetComponent<Renderer>() != null)
        {
            this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.black);
        }
        Renderer[] rend = this.gameObject.transform.GetComponentsInChildren<Renderer>();
        foreach (Renderer item in rend)
        {
            item.material.SetColor("_EmissionColor", Color.black);
        }
    }

    public abstract float VisionAngle(); //igen, propertyként kéne...
	#endregion
		
	bool IsInLineOfSight(Transform obj)
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

		while (ind < tf.Count) 
		{
			if (!tf [ind].material.SeeTrough ()) {
				return false;
			}
			ind++;
		}
		
		return true;
		//if (tf.Count != 0) {
		//	do {
		//		if (!tf[ind].material.SeeTrough())
		//		{
		//			return false;
		//		}
		//		ind++;
		//	} while(ind < tf.Count);
		//}
		//return true;
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
		
    //átgondolni  jelenlegi működést zombikra (asszem szar)
    public void TakeDamage(int damage)
	{
		if(damage > 0)
			Health = Health - (damage * Condition.DamageMultiplier());
		if (Health <= 0) 
		{
			if(Condition!=condition_dead)
				deadSound.Play();
			Condition = condition_dead;
            
			//Debug.Log ("Meghaltam, segítség!");
		}
		
	}
		
	public void KnockOut()
	{
		Condition = Condition.ChangeToKnockedOut (this);
	}

	public void Blinding()
	{
		Condition = Condition.ChangeToBlind (this);
	}

	public bool Carry()
	{
		return (Condition.CarryAble ());
	}

	public void GetNoise(int noiseMeter, Vector3 location)
	{
		Condition.ReactToNoise (this, noiseMeter, location);
	}

	public void DecreaseSuspicion()
	{
		//Debug.Log ("Suspicion: " + Suspicion);
		if (Suspicion > 0) {
			Suspicion -= (int)SuspicionDecrease;
			Condition.SuspicionDecreaseOverTime (this);
		} 
		else 
		{
			Suspicion = 0;
		}
	}
		
    public void DetachEquipment(Equipment item)
    {
		int i = -1;
		do
		{
			i++;
        }
		while((i < e.Count) && (!(e[i] is Equipment) || (e[i] as Equipment).Kod != item.Kod));

		if (i < e.Count) 
		{
			if (item.Kod >= 10 && item.Kod <= 20)
            {
				Condition.DoorDisable (item.Kod);
			}
           
            e.RemoveAt (i);
		}
        
    }

	public void DetachEquipment(Valuable item)
	{
		int i = -1;
		do
		{
			i++;
		}
		while((i >= e.Count) || ((e[i] is Valuable) && (e[i] as Valuable).value == item.value));

		if (i < e.Count)
			e.RemoveAt (i);
	}
    
}
