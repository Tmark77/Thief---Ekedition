using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {
    public Canvas mainMenu;
    public Canvas quitMenu;
    public Canvas inputMenu;

    public Transform controlPanel;
    Event keyEvent;
    KeyCode newKey;
    Text buttonText;
    bool waitingForKey;

    // Use this for initialization
    void Start () {
        quitMenu = quitMenu.GetComponent<Canvas>();
        quitMenu = quitMenu.GetComponent<Canvas>();
        inputMenu = inputMenu.GetComponent<Canvas>();

        mainMenu.enabled = true;
        quitMenu.enabled = false;
        inputMenu.enabled = false;

        controlPanel = transform.Find("InputMenu_panel");
        controlPanel.gameObject.SetActive(false);
        waitingForKey = false;

        IterateButtons();
    }

    void Update()
    {
        //kell még módosítani
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.enabled = true;
            quitMenu.enabled = false;
            inputMenu.enabled = false;
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
        }

        yield return null;
    }

    public void ExitPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = true;
        inputMenu.enabled = false;

    }
    public void NoPress()
    {
        mainMenu.enabled = true;
        quitMenu.enabled = false;
        inputMenu.enabled = false;
    }

    public void InputPress()
    {
        mainMenu.enabled = false;
        quitMenu.enabled = false;
        inputMenu.enabled = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("Character");

    }

    public void ExitGame()
    {
        Application.Quit(); 
    }

    public void IterateButtons()
    {
        Debug.Log(controlPanel.childCount);
        Debug.Log('a');
        for (int i = 0; i < controlPanel.childCount; i++)
        {
            if (controlPanel.GetChild(i).name == "Forward_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.forward.ToString();
            else if (controlPanel.GetChild(i).name == "Backward_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.backward.ToString();
            else if (controlPanel.GetChild(i).name == "Left_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.left.ToString();
            else if (controlPanel.GetChild(i).name == "Right_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.right.ToString();
            else if (controlPanel.GetChild(i).name == "Jump_button")
                controlPanel.GetChild(i).GetComponentInChildren<Text>().text = GameManager.GM.jump.ToString();
        }
    }
}
