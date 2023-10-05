using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingMenu : MenuParent
{
    public bool isPaused = false;

    private void OnEnable()
    {
        isPaused = true;
        FindAnyObjectByType<GameManager>().currentMenuOpened = UIMenu.Pause;
    }
    private void OnDisable()
    {
        FindAnyObjectByType<GameManager>().lastMenuOpened = UIMenu.Pause;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseDaGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        FindAnyObjectByType<GameManager>().countingTime = false;
    }

    public void UnpauseDaGame()
    {
        gameObject.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        FindAnyObjectByType<GameManager>().countingTime = true;
    }
}
