using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus
{
    private static Menus _instance;

    private Menus() { }

    public static Menus Instance
    {
        get
        {
            if (_instance == null)
                _instance = new Menus();

            return _instance;
        }
    }
    

    public Canvas MainMenu { get; set; }
    public Canvas QuitMenu { get; set; }
    public Canvas InputMenu { get; set; }
    public Canvas PauseMenu { get; set; }
    public Canvas Menu { get; set; }
}
