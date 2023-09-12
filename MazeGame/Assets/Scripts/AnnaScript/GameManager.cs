using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private InputControls controls;


    private void Awake()
    {
        controls = new InputControls();
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            PauseDaGame();
        }
        if (controls.Player1.Pause.triggered)
        {
            pauseMenu.SetActive(true);
            PauseDaGame();
        }
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
        //i have zero fuckin idea why this keeps running in update even though it doesn't satisfy the if statement
        Debug.Log("Ideally, when this function runs, it should freeze the scene and open the pause menu and also not constantly run in update.");
    }

    public void UnpauseDaGame()
    {
        pauseMenu.SetActive(false);
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
