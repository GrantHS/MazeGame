using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PausePanel;

    public bool isPasued;
    // Update is called once per frame
    private void Awake()
    {
        PausePanel.SetActive(false);
        isPasued = false;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPasued)
            {
                Continue();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        isPasued = true;
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        isPasued = false;
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
}
