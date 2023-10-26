using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum UIMenu
{
    MainMenu,
    LevelSelect,
    Options,
    Pause,
    Victory
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; //singleton
    public InputControls controls;

    public GameObject farmerAI;
    private FarmerAi farmerAiScript;

    //controlling menus
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private GameObject victoryScreen;

    public UIMenu secondLastMenuOpened;
    public UIMenu lastMenuOpened;
    public UIMenu currentMenuOpened;
    [SerializeField] private Dictionary<UIMenu, GameObject> menuDictionary = new Dictionary<UIMenu, GameObject>();
     
    public bool levelFinished;
    
    //Time Counter
    public float playerTime;
    public bool countingTime;

    //Level Objects
    public GameObject playerSpawn;
    public GameObject farmerSpawn;
    public GameObject yellowKey;
    public GameObject redKey;
    public GameObject orangeKey;
    //probably placeholder but whatev
    public GameObject currentLevel;
    public GameObject tutorialLevel;
    public GameObject firstLevel;

    private void Awake()
    {
        farmerAiScript = new FarmerAi();
        //singleton setup
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        //Application.targetFrameRate = -1; //this is to make the game not lag
        Application.targetFrameRate = 300;
        //QualitySettings.vSyncCount = 0;

        controls = new InputControls();
        countingTime = true;

        //adding menus into the menu dictionary
        menuDictionary.Add(UIMenu.LevelSelect, levelSelectMenu);
        menuDictionary.Add(UIMenu.MainMenu, mainMenu);
        menuDictionary.Add(UIMenu.Options, optionsMenu);
        menuDictionary.Add(UIMenu.Pause, pauseMenu);
        menuDictionary.Add(UIMenu.Victory, victoryScreen);

        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        victoryScreen.SetActive(false);

        firstLevel.SetActive(false);
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();


    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Escape)) //currently doesn't work for some reason
        {
            if (!isPaused)
            {
                PauseDaGame();
            }
            else
            {
                Debug.Log("this happened");
                UnpauseDaGame();
            }
        }
        if (controls.Player1.Pause.triggered)
        {
            if (isPaused == false)
            {
                PauseDaGame();
            }
            else
            {
                Debug.Log("this happened");
                UnpauseDaGame();
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Escape) && levelFinished == false) 
        {
            pauseMenu.SetActive(true);
            countingTime = false;
        }
        if (controls.Player1.Pause.triggered && levelFinished == false)
        {
            pauseMenu.SetActive(true);
            countingTime = false;
        }
        
        if(countingTime == true)
        {
            playerTime += Time.deltaTime;
            TimeCounter();
        }
    }

    public void EnableMenu(UIMenu menu)
    {
        if (!menuDictionary.ContainsKey(menu))
            return;

        GameObject menuToEnable = menuDictionary[menu];
        menuToEnable.SetActive(true);
    }

    public void DisableMenu(UIMenu menu)
    {
        if (!menuDictionary.ContainsKey(menu))
            return;
        GameObject menuToDisable = menuDictionary[menu];
        menuToDisable.SetActive(false);
    }
    public void BackButton() //goes back to the previous menu accessed
    {
        DisableMenu(currentMenuOpened);
        EnableMenu(lastMenuOpened);       
        
    }

    /*public void PauseDaGame()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isPaused = !isPaused;
        countingTime = !countingTime;
        Time.timeScale = 0;
        currentMenuOpened = UIMenu.Pause;
    }

    public void UnpauseDaGame()
    {
        pauseMenu.SetActive(false);
        isPaused = !isPaused;
        countingTime = !countingTime;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        lastMenuOpened = UIMenu.Pause;
    }*/
    
    public string TimeCounter()
    {
        int minutes = Mathf.FloorToInt(playerTime/60f);
        int seconds = Mathf.FloorToInt(playerTime - minutes * 60f);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        return niceTime;
    }

    public void RestartLevel()
    {
        levelFinished = false;
        Time.timeScale = 1;

        GameObject.Find("Player").transform.SetPositionAndRotation(playerSpawn.transform.position, playerSpawn.transform.rotation);
        // GameObject.Find("FarmerAI").transform.SetPositionAndRotation(farmerSpawn.transform.position, farmerSpawn.transform.rotation);
        // farmerAI.transform.SetPositionAndRotation(farmerSpawn.transform.position, farmerSpawn.transform.rotation);
        StartCoroutine(spawnFarmer());
        
        farmerAiScript.wayPointSet = false;
        playerTime = 0;
        countingTime = true;

        DisableMenu(currentMenuOpened);
        lastMenuOpened = currentMenuOpened;

       // Debug.Log("Farmer Respawned at: " + GameObject.Find("FarmerAI").transform.position);
        Debug.Log("Player Respawned at: " + GameObject.Find("Player").transform.position);

        FindAnyObjectByType<PushDoor>().yellowKey = false;
        FindAnyObjectByType<PushDoor>().orangeKey = false;
        FindAnyObjectByType<PushDoor>().redKey = false;

        yellowKey.SetActive(true);
        orangeKey.SetActive(true);
        redKey.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit(); //use for quitting in builds
        //UnityEditor.EditorApplication.isPlaying = false; //use for quitting play mode
    }

    public void OpenOptionsMenu()
    {
        lastMenuOpened = currentMenuOpened;

        Time.timeScale = 0;

        DisableMenu(lastMenuOpened);
        optionsMenu.SetActive(true);
    }
    public void CloseOptionsMenu() //unused at the moment
    {
        EnableMenu(lastMenuOpened);
        optionsMenu.SetActive(false);
    }
    public void OpenLevelSelectMenu()
    {
        lastMenuOpened = currentMenuOpened;

        Time.timeScale = 0;

        DisableMenu(lastMenuOpened);
        levelSelectMenu.SetActive(true);
    }

    public void LevelSelectToVictoryScreenTransition() //this is a placeholder
    {
        lastMenuOpened = currentMenuOpened;
        DisableMenu(lastMenuOpened);
        victoryScreen.SetActive(true);
    }

    private IEnumerator spawnFarmer()
    {
        farmerAI.gameObject.SetActive(false);
        farmerAI.GetComponent<FarmerAi>().wayPointSet = false;
        yield return new WaitForSeconds(1);
         farmerAI.transform.SetPositionAndRotation(farmerSpawn.transform.position, farmerSpawn.transform.rotation);
        // farmerAI.transform.position = farmerSpawn.transform.position;
       
        farmerAI.gameObject.SetActive(true);
       

    }
}
