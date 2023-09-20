using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
            FindAnyObjectByType<GameManager>().countingTime = false;
            Cursor.lockState = CursorLockMode.None;
            victoryScreen.SetActive(true);
        }
    }
}
