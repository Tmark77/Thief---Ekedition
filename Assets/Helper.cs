using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour {

    [SerializeField] public Text information;

    // Use this for initialization
    void Start () {
        counter = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;
    }

    float counter;
    private void OnTriggerEnter(Collider other)
    {
        counter = 3f;
        if (QuestManagerScript.completed1 == true && QuestManagerScript.completed2 == true && QuestManagerScript.completed3 == true)
        {
            if (counter < 0)
            {
                PlayerHealth.restart = true;
            }
            information.text = "LEVEL COMPLETED!";

        }
        else if (QuestManagerScript.completed1 == true && QuestManagerScript.completed2 == true && QuestManagerScript.completed3 == false)
        {
            information.text = "LEVEL FAILED!";
        }
        else
        {
            information.text = "Collect at least 200 gold, steal the big Gem, and you can kill only one Guard. Good Luck!";
        }       
    }

    private void OnTriggerExit(Collider other)
    {
        information.text = string.Empty;
    }
}
