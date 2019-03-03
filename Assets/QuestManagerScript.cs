using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManagerScript : MonoBehaviour {

    [SerializeField] public Text obj1;
    [SerializeField] public Text obj2;
    [SerializeField] public Text obj3;

    public static bool completed1;
    public static bool completed2;
    public static bool completed3;

    public List<Creature> guards;

    PlayerInventory inv;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Obj1();
        Obj2();
        Obj3();
    }

    void Obj1()
    {
        if (PlayerInventory.GEMS >= 1000)
        {
            obj1.text = "Completed!";
            completed1 = true;
        }
    }

    void Obj2()
    {
        if (PlayerInventory.GOLDS >= 200 && PlayerInventory.Others >= 200)
        {
            obj2.text = "Completed!";
            completed2 = true;
        }
    }

    int sumDeadGuards;
    void Obj3()
    {
        sumDeadGuards = 0;
        foreach (Creature g in guards)
        {
            if (g.Condition == g.condition_dead)
            {
                sumDeadGuards++;
            }
        }

        if (sumDeadGuards > 2)
        {
            obj3.text = "Failed!";
            completed3 = false;
        }
        else
        {
            completed3 = true;
        }
    }
}
