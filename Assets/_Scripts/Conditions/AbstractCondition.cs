using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class AbstractCondition : MonoBehaviour
{
    public void Awake()
    {
        this.agent = this.transform.parent.GetComponent<NavMeshAgent>();
    }

    public abstract void Init(Creature creature);

    protected NavMeshAgent agent;

    public virtual int DamageMultiplier() //gyanútlan ellenfél nagyobb sebzést kaphat, alap értéke 1
    {
        return 1; //1 az alap értéke, szorzóként működik
    }

    public virtual bool CarryAble()
    {
        return false;
    }

    //--------------------------------------------------
    public abstract AbstractCondition ChangeToKnockedOut(Creature creature);

    public abstract AbstractCondition ChangeToBlind(Creature creature);

    public abstract void SuspicionDecreaseOverTime(Creature creature);

    //kell hogy egy célpont felé hogyan reagál
    public abstract void PatrolBehaviour(Creature creature, ref int index);

    public abstract void ReactToNoise(Creature creature, int noiseMeter, Vector3 location);

    public abstract void ReactToView(Creature creature, int H, int C, int F);

    public virtual bool CanUseThings()
    {
        return true;
    }


    public void DoorAvaible(int doorID)
    {
        this.Awake();
        if (CanUseThings() && ((agent.areaMask & (int)Math.Pow(2, doorID)) != (int)Math.Pow(2, doorID)))
        {
            agent.areaMask = agent.areaMask + (int)Math.Pow(2, doorID);
            //Debug.Log("avaible: " + doorID);
        }

    }

    //az útvonaltervből kiveszi az ajtót, tehát nem mehet át az adott ajtón a lény
    public void DoorDisable(int doorID)
    {
        if ((agent.areaMask & (int)Math.Pow(2, doorID)) == (int)Math.Pow(2, doorID))
        {
            agent.areaMask = agent.areaMask - (int)Math.Pow(2, doorID);
            Debug.Log("denied: " + doorID);
        }

    }
    
}
