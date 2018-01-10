using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public Canvas mainMenu;
    public Canvas quitMenu;

	// Use this for initialization
	void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();

        mainMenu.enabled = true;
        quitMenu.enabled = false;
    }
	
    public void ExitPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = true;

    }
    public void NoPress()
    {
        mainMenu.enabled = true;
        quitMenu.enabled = false;
    }

    public void StartLevel()
    {
        Debug.Log("asd");
        SceneManager.LoadScene("Character");

    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
