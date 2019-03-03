using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Speed
{

    public Speed(int speed, int angularSpeed, int acceleration)
    {
        this.speed = speed;
        this.angularSpeed = angularSpeed;
        this.acceleration = acceleration;
    }

    public int speed;
    public int angularSpeed;
    public int acceleration;

    public void SetToNavMeshAgent(NavMeshAgent agent)
    {
        agent.speed = this.speed;
        agent.angularSpeed = this.angularSpeed;
        agent.acceleration = this.acceleration;
    }
	
}
