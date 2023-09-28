using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public UIMenu menu;
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
        
    }

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
        }
    } 
    public void ScrollRight()
    {
        if(levelScrollRect != null)
        {
            if(levelScrollRect.horizontalNormalizedPosition <= 1f)
            {
                levelScrollRect.horizontalNormalizedPosition += scrollSpeed;
            }
        }
    }
}
