using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MenuParent
{
    //UI Related Variables
    public RectTransform[] levelThumbnailList;
    [SerializeField] private ScrollRect levelScrollRect;
    [SerializeField] private RectTransform levelViewportTransform;
    [SerializeField] private RectTransform levelContentTransform;
    [SerializeField] private HorizontalLayoutGroup levelHLG;

    //[SerializeField] private TextMeshProUGUI Level_1_timeText;
    //[SerializeField] private TextMeshProUGUI Level_2_timeText;


    [SerializeField] private LevelScrollButton leftButton;
    [SerializeField] private LevelScrollButton rightButton;

    public TextMeshProUGUI selectedLevelNameText;

    //Selecting the Level Variables
    public int selectedLevelIndex = 0;
    public GameObject selectedMazeLevel;
    public GameObject selectedPlayerSpawn;
    public GameObject selectedFarmerSpawn;
    public GameObject selectedYellowKey;
    public GameObject selectedRedKey;
    public GameObject selectedOrangeKey;

    private void Awake()
    {
        int itemsToAdd = Mathf.CeilToInt(levelViewportTransform.rect.width / (levelThumbnailList[0].rect.width + levelHLG.spacing));

        levelContentTransform.localPosition = new Vector3((0 - (levelThumbnailList[0].rect.width + levelHLG.spacing)*itemsToAdd), 
            levelContentTransform.localPosition.y, levelContentTransform.localPosition.z);

        selectedLevelNameText.text = levelThumbnailList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
    }

    private void OnEnable()
    {
       GameManager.Instance.currentMenuOpened = UIMenu.LevelSelect;
        DisplayTime();
        Time.timeScale = 0;
    }
    public void ScrollLeft() //currently unused
    {
        
        selectedLevelIndex--;
        //Debug.Log(selectedLevelIndex);

        if (selectedLevelIndex < 0)
        {
            selectedLevelIndex = levelThumbnailList.Length - 1;
        }
        Debug.Log(selectedLevelIndex);

        SelectLevel();

    }

    private void DisplayTime()
    {
        //Level_1_timeText.text = "Best Time: " + GameManager.Instance.TimeCounter();
       // Level_2_timeText.text = "Best Time: " + GameManager.Instance.TimeCounter();
    }





    public void ScrollRight() //currently unused
    {
        
        selectedLevelIndex++;
        //Debug.Log(selectedLevelIndex);
        
        if (selectedLevelIndex > levelThumbnailList.Length - 1)
        {
            selectedLevelIndex = 0;
        }
        Debug.Log(selectedLevelIndex);

        SelectLevel();
    }

    public void SelectLevel()
    {
        selectedMazeLevel = GameManager.Instance.levelList[selectedLevelIndex];

        levelThumbnailList[selectedLevelIndex].GetComponent<LevelInfoHolder>().OnLevelSelected();

        selectedLevelNameText.text = levelThumbnailList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
    }

    public void PlayLevel()
    {
        GameManager.Instance.EnableLevel(GameManager.Instance.currentLevel);
        this.gameObject.SetActive(false); //not sure why it's not disabling rn so this is here
        GameManager.Instance.RestartLevel();
    }
}
