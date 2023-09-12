using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject pauseMenu;
    [SerializeField] private InputControls controls;


    private void Awake()
    {
        controls = new InputControls();
        pauseMenu.SetActive(false);
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            pauseMenu.SetActive(true);
            PauseDaGame();
        }
        if (Input.GetButtonDown("Pause"))
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
        
    }
}
