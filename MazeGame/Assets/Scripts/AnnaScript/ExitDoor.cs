using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen;

    private void Awake()
    {
        victoryScreen.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {    

        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.levelFinished = true;
            GameManager.Instance.countingTime = false;
            Cursor.lockState = CursorLockMode.None;
            victoryScreen.SetActive(true);
            GameManager.Instance.LevelCompleted();
        }
    }
}
