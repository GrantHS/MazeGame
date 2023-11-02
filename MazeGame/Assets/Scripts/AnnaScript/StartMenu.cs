using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu : MenuParent
{
    void Awake()
    {
        //maybe make it so that the start menu checks for any save data here
    }

    private void OnEnable()
    {
        GameManager.Instance.currentMenuOpened = UIMenu.StartMenu;
        
    }

    // This is so level things don't happen while the start menu is up
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        GameManager.Instance.countingTime = false;
    }
}
