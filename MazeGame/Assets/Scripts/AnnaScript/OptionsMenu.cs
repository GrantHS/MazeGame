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
        GameManager.Instance.currentMenuOpened = UIMenu.Options;
        GameManager.Instance.countingTime = !GameManager.Instance.countingTime;
        Cursor.lockState = CursorLockMode.None;
    }

    //private void OnDisable() => GameManager.Instance.lastMenuOpened = UIMenu.Options;

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

    public void FullscreenToggle()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    public void WindowedToggle()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}
