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

    // Update is called once per frame
    void Update()
    {
        
    }
}
