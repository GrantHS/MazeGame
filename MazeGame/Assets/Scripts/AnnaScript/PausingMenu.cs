using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingMenu : MenuParent
{
    public bool isPaused = false;

    private void OnEnable()
    {
        isPaused = true;
        GameManager.Instance.currentMenuOpened = UIMenu.Pause;
        PauseDaGame();
    }
    private void OnDisable()
    {
        //FindAnyObjectByType<GameManager>().lastMenuOpened = UIMenu.Pause;
        UnpauseDaGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseDaGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        GameManager.Instance.countingTime = false;
    }

    public void UnpauseDaGame()
    {
        if (gameObject.activeSelf == true)
            gameObject.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        GameManager.Instance.countingTime = true;
    }
}
