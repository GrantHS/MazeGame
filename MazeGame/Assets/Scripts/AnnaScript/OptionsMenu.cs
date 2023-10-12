using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MenuParent
{
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    private void Awake()
    {
        mouseSensitivitySlider.value = FindAnyObjectByType<MouseLook>().mouseSen / 10;
        sensitivityText.text = mouseSensitivitySlider.value.ToString();
    }

    private void OnEnable()
    {
        FindAnyObjectByType<GameManager>().currentMenuOpened = UIMenu.Options;
        FindAnyObjectByType<GameManager>().countingTime = !FindAnyObjectByType<GameManager>().countingTime;
        Cursor.lockState = CursorLockMode.None;
    }

    //private void OnDisable() => FindAnyObjectByType<GameManager>().lastMenuOpened = UIMenu.Options;

    public void HUDOn()
    {
        gameplayHUD.SetActive(true);
        gameplayHUD.GetComponent<Canvas>().sortingOrder = GetComponent<Canvas>().sortingOrder - 2; //number probably subject to change
    }
    public void HUDOff() => gameplayHUD.SetActive(false);

    public void MouseSensitivitySlider()
    {
        float mouseSensitivity = mouseSensitivitySlider.value * 10;
        FindAnyObjectByType<MouseLook>().mouseSen = mouseSensitivity;
        sensitivityText.text = mouseSensitivitySlider.value.ToString();
    }
}
