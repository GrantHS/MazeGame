using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameSceneManager controls;
    private bool isPaused;




    private void Awake()
    {
        controls = new GameSceneManager();
    }



    // Update is called once per frame
    void Update()
    {
        PauseDaGame();
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void PauseDaGame()
    {
        Debug.Log("The game will pause here and UI elements will be displayed");
    }
}
