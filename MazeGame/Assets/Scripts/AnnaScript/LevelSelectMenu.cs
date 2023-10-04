using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MenuParent
{
    public RectTransform[] levelList;
    [SerializeField] private ScrollRect levelScrollRect;
    [SerializeField] private RectTransform levelViewportTransform;
    [SerializeField] private RectTransform levelContentTransform;
    [SerializeField] private HorizontalLayoutGroup levelHLG;


    [SerializeField] private LevelScrollButton leftButton;
    [SerializeField] private LevelScrollButton rightButton;

    [SerializeField] private float scrollSpeed = 0.01f;
    private bool isUpdated;
    private Vector2 scrollVelocity;

    [SerializeField] private int selectedLevelIndex = 0;
    public TextMeshProUGUI selectedLevelNameText;

    private void Awake()
    {
        isUpdated = false;
        scrollVelocity = Vector2.zero;
        int itemsToAdd = Mathf.CeilToInt(levelViewportTransform.rect.width / (levelList[0].rect.width + levelHLG.spacing));

        for (int i = 0; i < itemsToAdd; i++)
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
        }

        levelContentTransform.localPosition = new Vector3((0 - (levelList[0].rect.width + levelHLG.spacing)*itemsToAdd), 
            levelContentTransform.localPosition.y, levelContentTransform.localPosition.z);

        selectedLevelNameText.text = levelList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
        //please take note of anchored position, perhaps that might help in getting the element centered when selected
    }

    private void OnEnable() => FindAnyObjectByType<GameManager>().currentMenuOpened = UIMenu.LevelSelect;
    private void OnDisable() => FindAnyObjectByType<GameManager>().lastMenuOpened = UIMenu.LevelSelect;

    private void Update()
    {
        if(isUpdated)
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
        }
        
        if (leftButton != null)
        {
            if (leftButton.isDown)
            {
                ScrollLeft();
            }
        }
        if (rightButton != null)
        {
            if (rightButton.isDown)
            {
                ScrollRight();
            }
        }
    }

    public void ScrollLeft()
    {
        if(levelScrollRect != null)
        {
            if(levelScrollRect.horizontalNormalizedPosition >= 0f)
            {
                levelScrollRect.horizontalNormalizedPosition -= scrollSpeed;
            }

            selectedLevelIndex--;
            if(selectedLevelIndex < 0)
            {
                selectedLevelIndex = levelList.Length - 1;
            }
            selectedLevelNameText.text = levelList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
        }
    } 
    public void ScrollRight()
    {
        if(levelScrollRect != null)
        {
            if(levelScrollRect.horizontalNormalizedPosition <= 2f)
            {
                levelScrollRect.horizontalNormalizedPosition += scrollSpeed;
            }

            selectedLevelIndex++;
            if (selectedLevelIndex > levelList.Length - 1)
            {
                selectedLevelIndex = 0;
            }
            selectedLevelNameText.text = levelList[selectedLevelIndex].GetComponent<LevelInfoHolder>().levelInfo.levelName;
        }
    }
}
