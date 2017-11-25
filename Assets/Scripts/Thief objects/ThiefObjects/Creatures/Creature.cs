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

	int suspicion;
	public int Suspicion
	{
		get{ return suspicion; }
		set{ suspicion = value;}
	}

	public List<Collectible> KnownObjects = new List<Collectible> ();

	//Állapottipus
}
