using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreen : MenuParent
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject dancingGif;
    private void OnEnable()
    {
        DisplayTime();

        dancingGif.GetComponent<Animator>().Play("Dancing", 0);

        Time.timeScale = 0;
        GameManager.Instance.currentMenuOpened = UIMenu.Victory;
    }

    private void DisplayTime()
    {
        timeText.text = "Time: " + GameManager.Instance.TimeCounter();
    }

    
}
