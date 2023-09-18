using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public enum UIMenu
{
    MainMenu,
    LevelSelect,
    Options,
    Pause,
    Victory
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private InputControls controls;


    private void Awake()
    {
        controls = new InputControls();
        pauseMenu.SetActive(false);
        //optionsMenu.SetActive(false);
    }
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();


    // Update is called once per frame
    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.Escape)) //currently doesn't work for some reason
        {
            if (!isPaused)
            {
                PauseDaGame();
            }
            else
            {
                Debug.Log("this happened");
                UnpauseDaGame();
            }
        }
        if (controls.Player1.Pause.triggered)
        {
            if (isPaused == false)
            {
                PauseDaGame();
            }
            else
            {
                Debug.Log("this happened");
                UnpauseDaGame();
            }
        }*/
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseDaGame();
        }
        if (controls.Player1.Pause.triggered)
        {
            PauseDaGame();
        }
    }
    public void PauseDaGame()
    {
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        isPaused = !isPaused;
        Time.timeScale = 0;
    }

    public void UnpauseDaGame()
    {
        pauseMenu.SetActive(false);
        isPaused = !isPaused;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }
    
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }
    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }
}
