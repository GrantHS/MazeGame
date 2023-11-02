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


    [SerializeField] private LevelScrollButton leftButton;
    [SerializeField] private LevelScrollButton rightButton;

    [SerializeField] private float scrollSpeed = 0.01f;
    private bool isUpdated;
    private Vector2 scrollVelocity;

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
        isUpdated = false;
        scrollVelocity = Vector2.zero;
        int itemsToAdd = Mathf.CeilToInt(levelViewportTransform.rect.width / (levelThumbnailList[0].rect.width + levelHLG.spacing));

        /*for (int i = 0; i < itemsToAdd; i++)
        {
            RectTransform rt = Instantiate(levelList[i % levelList.Length], levelContentTransform);
            rt.SetAsLastSibling();
        }

        for (int i = 0; i < itemsToAdd; i++)
        {
            int num = levelList.Length - i - 1;
            while (num < 0)
            {
                num += levelList.Length;
            }
            RectTransform rt = Instantiate(levelList[num], levelContentTransform);
            rt.SetAsFirstSibling();
        }*/

        levelContentTransform.localPosition = new Vector3((0 - (levelThumbnailList[0].rect.width + levelHLG.spacing)*itemsToAdd), 
            levelContentTransform.localPosition.y, levelContentTransform.localPosition.z);

        selectedLevelNameText.text = levelThumbnailList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
        //please take note of anchored position, perhaps that might help in getting the element centered when selected
    }

    private void OnEnable() => GameManager.Instance.currentMenuOpened = UIMenu.LevelSelect;

    private void Update()
    {
        /*if(isUpdated)
        {
            isUpdated = false;
            levelScrollRect.velocity = scrollVelocity;
        }
        if (levelContentTransform.localPosition.x > 0)
        {
            Canvas.ForceUpdateCanvases();
            scrollVelocity = levelScrollRect.velocity;
            levelContentTransform.localPosition -= new Vector3(levelList.Length * (levelList[0].rect.width + levelHLG.spacing), 0, 0);
            isUpdated = true;
        }
        if (levelContentTransform.localPosition.x < 0 - (levelList.Length * (levelList[0].rect.width + levelHLG.spacing)))
        {
            Canvas.ForceUpdateCanvases();
            scrollVelocity = levelScrollRect.velocity;
            levelContentTransform.localPosition += new Vector3(levelList.Length * (levelList[0].rect.width + levelHLG.spacing), 0, 0);
            isUpdated = true;
        }*/
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

        selectedMazeLevel = GameManager.Instance.levelList[selectedLevelIndex];
        selectedLevelNameText.text = levelThumbnailList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;

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

        selectedMazeLevel = GameManager.Instance.levelList[selectedLevelIndex];
        selectedLevelNameText.text = levelThumbnailList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
    }

    public void SelectLevel()
    {
        GameManager.Instance.currentLevel = selectedMazeLevel;
        GameManager.Instance.playerSpawn = selectedPlayerSpawn;
        GameManager.Instance.farmerSpawn = selectedFarmerSpawn;
        GameManager.Instance.yellowKey = selectedYellowKey;
        GameManager.Instance.redKey = selectedRedKey;
        GameManager.Instance.orangeKey = selectedOrangeKey;

        GameManager.Instance.EnableLevel(GameManager.Instance.currentLevel);

        GameManager.Instance.RestartLevel();
    }
}
