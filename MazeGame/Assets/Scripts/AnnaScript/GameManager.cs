using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum UIMenu
{
    StartMenu,
    LevelSelect,
    Options,
    Pause,
    Victory
}

public class GameManager : MonoBehaviour, IDataStuff
{
    public static GameManager Instance; //singleton
    public InputControls controls;

    public GameObject farmerAI;
    private FarmerAi farmerAiScript;

    //controlling menus
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private GameObject victoryScreen;

    public UIMenu lastMenuOpened;
    public UIMenu currentMenuOpened;
    [SerializeField] private Dictionary<UIMenu, GameObject> menuDictionary = new Dictionary<UIMenu, GameObject>();
     
    public bool levelFinished;
    public Text bestTimeUI;
    //Time Counter
    public float playerTime;
    public bool countingTime;

    private float FinishTime;

    //Level Objects
    public GameObject playerSpawn;
    public GameObject farmerSpawn;
    public GameObject yellowKey;
    public GameObject redKey;
    public GameObject orangeKey;
    //Selecting Levels
    public GameObject currentLevel;
    public GameObject tutorialLevel;
    public GameObject firstLevel;
    public List<GameObject> levelList = new List<GameObject>();


    // Rating Levels Time Objects
    public GameObject Starsprites_1, Starsprites_2, Starsprites_3;


    private void Awake()
    {
        LoadGame();

        farmerAiScript = new FarmerAi();
        //singleton setup
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        controls = new InputControls();
        countingTime = true;

        //adding menus into the menu dictionary
        menuDictionary.Add(UIMenu.LevelSelect, levelSelectMenu);
        menuDictionary.Add(UIMenu.StartMenu, startMenu);
        menuDictionary.Add(UIMenu.Options, optionsMenu);
        menuDictionary.Add(UIMenu.Pause, pauseMenu);
        menuDictionary.Add(UIMenu.Victory, victoryScreen);

        //adding levels into level list
        levelList.Add(tutorialLevel);
        levelList.Add(firstLevel);


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
            FinishTime = playerTime;
            
            TimeCounter();
        }
    }

    public void EnableLevel(GameObject level) //if this works correctly, it should enable the selected level and disable all others
    {
        if (!levelList.Contains(level))
            return;

        int levelIndex = levelList.IndexOf(level);

        foreach (GameObject levels in levelList)
        {
            levels.SetActive(false);
        }

        GameObject levelToEnable = levelList[levelIndex];
        levelToEnable.SetActive(true);
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
    
    public string TimeCounter()
    {
        int minutes = Mathf.FloorToInt(playerTime/60f);
        int seconds = Mathf.FloorToInt(playerTime - minutes * 60f);
       // Debug.Log("Current Time: " + playerTime);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        return niceTime;
    }

    public void RestartLevel()
    {
        levelFinished = false;
        Time.timeScale = 1;

        GameObject.Find("Player").transform.SetPositionAndRotation(playerSpawn.transform.position, playerSpawn.transform.rotation);
        StartCoroutine(spawnFarmer());
        
        farmerAiScript.wayPointSet = false;
        playerTime = 0;
        countingTime = true;
        Cursor.lockState = CursorLockMode.Locked;

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

    public void OpenStartMenu()
    {
        lastMenuOpened = currentMenuOpened;

        Time.timeScale = 0;

        DisableMenu(lastMenuOpened);
        startMenu.SetActive(true);
    }

    public void LevelSelectToStartMenuTransition() //this is a placeholder
    {
        lastMenuOpened = currentMenuOpened;
        DisableMenu(lastMenuOpened);
        startMenu.SetActive(true);
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

    public void LoadData(DataStuff data)
    {
        FinishTime = data.GameTime;
    }

    public void SaveData(ref DataStuff data)
    {
       data.GameTime = FinishTime;
    }

    public void SaveGame()
    {
        DataStuff data = new DataStuff();
        SaveData(ref data);

        // Save the data to PlayerPrefs
        PlayerPrefs.SetFloat("GameTime", data.GameTime);
        PlayerPrefs.Save();
    }
    public void LoadGame()
    {
        DataStuff data = new DataStuff();

        // Load the saved data from PlayerPrefs
        data.GameTime = PlayerPrefs.GetFloat("GameTime", 0f);

        LoadData(data);
    }

    private void OnApplicationQuit()
    {
        SaveGame();

    }

    public void LevelCompleted()
    {
        SaveGame();
        GiveRating();

    }


    private void GiveRating()
    {
        if (FinishTime < 45 )
        {
            StarsActive(3);
        }
       else if (FinishTime < 60)
        {
            StarsActive(2);
        }
       else if (FinishTime < 90)
        {
            StarsActive(1);
        }
    }

    private void StarsActive(int numstars)
    {
        Starsprites_1.SetActive(false);
        Starsprites_2.SetActive(false);
        Starsprites_3.SetActive(false);

        switch (numstars)
        {
            case 3:

                Starsprites_1.SetActive(true);
                Starsprites_2.SetActive(true);
                Starsprites_3.SetActive(true);
                break;

            case 2:

                Starsprites_1.SetActive(true);
                Starsprites_2.SetActive(true);

                break;

            case 1:

                Starsprites_1.SetActive(true);
                break;
            default:
                break;
        }


    }

    private void DisplayBestTime()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        // Check if the current completion time is better than the saved best time
        if (FinishTime < bestTime)
        {
            // Update the best time and save it
            bestTime = FinishTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            PlayerPrefs.Save();
        }

        // Display the best time on the canvas
        bestTimeUI.text = "Best Time: " + TimeCounter();
    }



}
