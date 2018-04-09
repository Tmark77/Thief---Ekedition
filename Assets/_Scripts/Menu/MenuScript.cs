using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class MenuScript : MonoBehaviour {
    public Canvas healthCanvas;
    public Canvas inventoryCanvas;

    public Canvas mainMenu;
    public Canvas quitMenu;
    public Canvas inputMenu;
    public Canvas pauseMenu;
    public Canvas menu;
    Menus menus = Menus.Instance;

    public Transform controlPanel;
    Event keyEvent;
    KeyCode newKey;
    Text buttonText;
    bool waitingForKey;
    public static bool isGameStarted;
    CursorLockMode cursorMode;

    SavingLoading save;
    private FirstPersonController player;
    

    // Use this for initialization
    void Start () {

        player = GameObject.Find("Player").GetComponent<FirstPersonController>();
        save = new SavingLoading();

        healthCanvas.GetComponent<Canvas>();
        inventoryCanvas.GetComponent<Canvas>();

        menus.QuitMenu = quitMenu.GetComponent<Canvas>();
        menus.InputMenu = inputMenu.GetComponent<Canvas>();
        menus.MainMenu = mainMenu.GetComponent<Canvas>();
        menus.PauseMenu = pauseMenu.GetComponent<Canvas>();
        menus.Menu = menu.GetComponent<Canvas>();
        RestartRoutine();
    }

    void RestartRoutine()
    {
        menus.MainMenu.enabled = true;
        menus.QuitMenu.enabled = false;
        menus.InputMenu.enabled = false;
        menus.PauseMenu.enabled = false;
        
        waitingForKey = false;
        isGameStarted = false;

        ////Screen.lockCursor = false;
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.lockState = cursorMode;
        //Cursor.visible = true;
        
        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        healthCanvas.gameObject.SetActive(false);
        inventoryCanvas.gameObject.SetActive(false);

        Time.timeScale = 0;
    }


    void Update()
    {
        if (Input.GetKeyDown(GameManager.GM.pause) && isGameStarted)
        {
            menus.Menu.enabled = true;
            menus.MainMenu.enabled = false;
            menus.QuitMenu.enabled = false;
            menus.InputMenu.enabled = false;
            menus.PauseMenu.enabled = true;
            healthCanvas.gameObject.SetActive(false);
            inventoryCanvas.gameObject.SetActive(false);

            //Time.timeScale = 0;
            //Cursor.lockState = cursorMode;
            //Cursor.visible = true;

            isGameStarted = false;
            player.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;


            Cursor.visible = true;
            Debug.Log("PAUSE");

        }

        if (PlayerHealth.restart)
        {
            menus.Menu.enabled = true;
            RestartRoutine();
            //menus.Menu.enabled = true;
            //menus.MainMenu.enabled = true;
            //menus.QuitMenu.enabled = false;
            //menus.InputMenu.enabled = false;
            //menus.PauseMenu.enabled = false;
            
            //healthCanvas.gameObject.SetActive(false);
            //inventoryCanvas.gameObject.SetActive(false);

            //Time.timeScale = 0;
            //Cursor.lockState = cursorMode;
            //Cursor.visible = true;
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
                GameManager.GM.sneak = newKey;
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
        menus.MainMenu.enabled = false;
        menus.QuitMenu.enabled = true;
        menus.InputMenu.enabled = false;
        menus.PauseMenu.enabled = false;


    }
    public void QuitPress()
    {
        menus.MainMenu.enabled = true;
        menus.QuitMenu.enabled = false;
        menus.InputMenu.enabled = false;
        menus.PauseMenu.enabled = false;
        isGameStarted = false;
        // újratölti az aktuális scene-t
        //DontDestroyOnLoad(gameObject);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NoPress()
    {
        menus.MainMenu.enabled = true;
        menus.QuitMenu.enabled = false;
        menus.InputMenu.enabled = false;
        menus.PauseMenu.enabled = false;
    }
    public void BackPress()
    {
        if (isGameStarted)
        {
            menus.MainMenu.enabled = false;
            menus.QuitMenu.enabled = false;
            menus.InputMenu.enabled = false;
            menus.PauseMenu.enabled = true;
        }
        else
        {
            menus.MainMenu.enabled = true;
            menus.QuitMenu.enabled = false;
            menus.InputMenu.enabled = false;
            menus.PauseMenu.enabled = false;
        }
        
    }

    public void InputPress()
    {
        menus.MainMenu.enabled = false;
        menus.QuitMenu.enabled = false;
        menus.InputMenu.enabled = true;
        menus.PauseMenu.enabled = false;
        IterateButtons();
    }

    public void StartLevel()
    {
        Debug.Log("Start");
        menus.Menu.enabled = false;
        isGameStarted = true;
        PlayerHealth.restart = false;
        
        healthCanvas.gameObject.SetActive(true);
        inventoryCanvas.gameObject.SetActive(true);
        Time.timeScale = 1.0f;
        player.enabled = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
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
