using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    public UIMenu menu;
    public List<GameObject> levels = new List<GameObject>();
    [SerializeField] private ScrollRect levelScrollRect;

    [SerializeField] private LevelScrollButton leftButton;
    [SerializeField] private LevelScrollButton rightButton;

    [SerializeField] private float scrollSpeed = 0.01f;
    private void Update()
    {
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
