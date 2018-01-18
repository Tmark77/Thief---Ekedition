using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;

    public KeyCode jump { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode sneak { get; set; }
    public KeyCode crounch { get; set; }
    public KeyCode run { get; set; }
    public KeyCode rightPeek { get; set; }
    public KeyCode leftPeek { get; set; }
    public KeyCode pause { get; set; }



    void Awake()
    {
        //Singleton
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        
        
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
		
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        sneak = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("sneakKey", "LeftControl"));
        crounch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("crounchKey", "X"));
        run = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("runKey", "LeftShift"));
        rightPeek = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightPeekKey", "E"));
        leftPeek = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftPeekKey", "Q"));
        pause = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("pauseKey", "P"));

    }

    void Start()
    {

    }

    void Update()
    {

    }
}
