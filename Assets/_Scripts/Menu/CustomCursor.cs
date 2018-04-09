using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public bool cursorEnabled = false;


	// Use this for initialization
	void Start () {
        Enable();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Disable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void Enable()
    {
        Cursor.SetCursor(this.cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
