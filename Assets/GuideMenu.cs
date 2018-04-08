using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideMenu : MonoBehaviour {

    public GameObject menu;
    bool isActive;

	// Use this for initialization
	void Start () {
        menu.SetActive(false);
        isActive = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.H))
        {
            isActive = !isActive;
            menu.SetActive(isActive);
        }
	}
}
