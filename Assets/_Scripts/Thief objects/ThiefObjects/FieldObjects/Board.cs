using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : DynamicFieldObject
{
    public Text informationText;
    public string information;
    static float counter;
    bool show;

	public override void Interaction(bool IsManualyOperated)
    {
        counter = 3f;
        show = true;
        informationText.text = information;
    }

    // Use this for initialization
    void Start () {
        counter = 3f;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (show)
        {
			counter -= Time.deltaTime;

			if (counter < 0f) {
				informationText.text = string.Empty;
			}

			if(informationText.text != information)
			{
				show = false;
			}

        }
	}
}
