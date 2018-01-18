using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public Canvas mainMenu;
    public Canvas quitMenu;
    public Canvas inputMenu;
    public Canvas pauseMenu;
    public Canvas menu;

    public Transform controlPanel;
    Event keyEvent;
    KeyCode newKey;
    Text buttonText;
    bool waitingForKey;
    bool isGameStarted;

    private AudioListener[] myListeners;

    // Use this for initialization
    void Start () {
        
        
       myListeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
       foreach (AudioListener thisListener in myListeners)
       {
           //Debug.Log(thisListener);
           if (thisListener.name == "Main Camera")
           {
               thisListener.enabled = true;
           }
           else
           {
               thisListener.enabled = false;
           }
       }

        
        Time.timeScale = 0;
        Cursor.visible = true;

        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        inputMenu = inputMenu.GetComponent<Canvas>();

        mainMenu.enabled = true;
        quitMenu.enabled = false;
        inputMenu.enabled = false;
        pauseMenu.enabled = false;

        //controlPanel = transform.Find("Layout");
        waitingForKey = false;
        isGameStarted = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(GameManager.GM.pause) && isGameStarted)
        {
            menu.enabled = true;
            mainMenu.enabled = false;
            quitMenu.enabled = false;
            inputMenu.enabled = false;
            pauseMenu.enabled = true;
        }
    }

    void OnGUI()
    {
        keyEvent = Event.current;
        
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }
    
    public void SendText(Text text)
    {
        buttonText = text;
    }
    
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }
    
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey();
        

        switch (keyName)
        {
            case "forward":
                GameManager.GM.forward = newKey; 
                buttonText.text = GameManager.GM.forward.ToString(); 
                PlayerPrefs.SetString("Forward_button", GameManager.GM.forward.ToString());
                break;
            case "backward":
                GameManager.GM.backward = newKey;
                buttonText.text = GameManager.GM.backward.ToString();
                PlayerPrefs.SetString("Backward_button", GameManager.GM.backward.ToString());
                break;
            case "left":
                GameManager.GM.left = newKey; 
                buttonText.text = GameManager.GM.left.ToString(); 
                PlayerPrefs.SetString("Left_button", GameManager.GM.left.ToString()); 
                break;
            case "right":
                GameManager.GM.right = newKey; 
                buttonText.text = GameManager.GM.right.ToString(); 
                PlayerPrefs.SetString("Right_button", GameManager.GM.right.ToString()); 
                break;
            case "jump":
                GameManager.GM.jump = newKey; 
                buttonText.text = GameManager.GM.jump.ToString(); 
                PlayerPrefs.SetString("Jump_button", GameManager.GM.jump.ToString()); 
                break;
            case "sneak":
                GameManager.GM.sneak= newKey;
                buttonText.text = GameManager.GM.sneak.ToString();
                PlayerPrefs.SetString("Sneak_button", GameManager.GM.sneak.ToString());
                break;
            case "crounch":
                GameManager.GM.crounch = newKey;
                buttonText.text = GameManager.GM.crounch.ToString();
                PlayerPrefs.SetString("Crounch_button", GameManager.GM.crounch.ToString());
                break;
            case "run":
                GameManager.GM.run = newKey;
                buttonText.text = GameManager.GM.run.ToString();
                PlayerPrefs.SetString("Run_button", GameManager.GM.run.ToString());
                break;
            case "rightPeek":
                GameManager.GM.rightPeek = newKey;
                buttonText.text = GameManager.GM.rightPeek.ToString();
                PlayerPrefs.SetString("RightPeek_button", GameManager.GM.rightPeek.ToString());
                break;
            case "leftPeak":
                GameManager.GM.leftPeek = newKey;
                buttonText.text = GameManager.GM.leftPeek.ToString();
                PlayerPrefs.SetString("leftPeek_button", GameManager.GM.leftPeek.ToString());
                break;
        }

        yield return null;
    }

    public void ExitPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = true;
        inputMenu.enabled = false;
        pauseMenu.enabled = false;


    }
    public void QuitPress()
    {
        mainMenu.enabled = true;
        quitMenu.enabled = false;
        inputMenu.enabled = false;
        pauseMenu.enabled = false;
        isGameStarted = false;

    }
    public void NoPress()
    {
        mainMenu.enabled = true;
        quitMenu.enabled = false;
        inputMenu.enabled = false;
        pauseMenu.enabled = false;
    }
    public void BackPress()
    {
        if (isGameStarted)
        {
            mainMenu.enabled = false;
            quitMenu.enabled = false;
            inputMenu.enabled = false;
            pauseMenu.enabled = true;
        }
        else
        {
            mainMenu.enabled = true;
            quitMenu.enabled = false;
            inputMenu.enabled = false;
            pauseMenu.enabled = false;
        }
        
    }

    public void InputPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = false;
        inputMenu.enabled = true;
        pauseMenu.enabled = false;
        IterateButtons();
    }

    public void StartLevel()
    {
        menu.enabled = false;
        isGameStarted = true;

        foreach (AudioListener thisListener in myListeners)
        {
            if (thisListener.name == "ClimbCam" && !thisListener.enabled)
            {
                thisListener.enabled = true;
            }
            else
            {
                thisListener.enabled = false;
            }
            Debug.Log(thisListener+" ||| "+thisListener.enabled);
        }
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene("TestLevel");
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }

    public void IterateButtons()
    {
        for (int i = 0; i < controlPanel.childCount; i++)
        {
            if (controlPanel.GetChild(i).name == "Sneak_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.sneak.ToString();
            else if (controlPanel.GetChild(i).name == "Crounch_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.crounch.ToString();
            else if (controlPanel.GetChild(i).name == "Run_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.run.ToString();
            else if (controlPanel.GetChild(i).name == "RightPeek_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.rightPeek.ToString();
            else if (controlPanel.GetChild(i).name == "LeftPeek_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.leftPeek.ToString();
            else if (controlPanel.GetChild(i).name == "Jump_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();
        }
    }
}
