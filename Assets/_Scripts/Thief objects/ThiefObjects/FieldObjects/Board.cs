using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : DynamicFieldObject
{
    public Text informationText;
    public string information;
    float counter;
    bool show;

    public override void Interaction(bool IsRightClicked)
    {
        counter = 10f;
        show = true;
        informationText.text = information;
    }

    // Use this for initialization
    void Start () {
        counter = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;

        if (show && counter < 0f)
        {
            informationText.text = string.Empty;
            show = false;
        }
	}
}
