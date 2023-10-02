using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private UIMenu menu;
    [SerializeField] private TextMeshProUGUI timeText;


    private void OnEnable()
    {
        DisplayTime();
    }

    private void DisplayTime()
    {
        timeText.text = "Time: " + FindAnyObjectByType<GameManager>().TimeCounter();
    }

    public void ExitGame()
    {
        Application.Quit(); //use for quitting in builds
        //UnityEditor.EditorApplication.isPlaying = false; //use for quitting play mode
    }
}
