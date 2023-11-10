using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MenuParent, IDataStuff
{
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;
    public GameObject MiniMap_Heater;

    public float mouseSensitivity;

    private void Awake()
    {
        LoadFromStorage();

      //  mouseSensitivitySlider.value = FindAnyObjectByType<MouseLook>().mouseSen / 10;
        sensitivityText.text = mouseSensitivitySlider.value.ToString();
    }

    //Save Game Stuff

  
    private void OnEnable()
    {
       // MiniMap_Heater.SetActive(true);
       GameManager.Instance.currentMenuOpened = UIMenu.Options;
       GameManager.Instance.countingTime = !GameManager.Instance.countingTime;
       Cursor.lockState = CursorLockMode.None;
    }

    public void HUDOn()
    {
        gameplayHUD.SetActive(true);
        gameplayHUD.GetComponent<Canvas>().sortingOrder = GetComponent<Canvas>().sortingOrder - 2; //number probably subject to change
        MiniMap_Heater.SetActive(true);
    }
    public void HUDOff()
    {
        gameplayHUD.SetActive(false);
        MiniMap_Heater.SetActive(false);
    }

    public void MouseSensitivitySlider()
    {
        mouseSensitivity = mouseSensitivitySlider.value * 10;
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


     //Save Game Stuff

    public void LoadData(DataStuff data)
    {
        //this.mouseSensitivity = data.MouseSens;
        data.MouseSens = this.mouseSensitivity;
    }

    public void SaveData(ref DataStuff data)
    {
        this.mouseSensitivity = data.MouseSens;
    }

    private void LoadFromStorage()
    {
        float savedSensitivity = PlayerPrefs.GetFloat("MouseSensitivity", 0.5f); // Use a default value (0.5f in this case) if not found
        mouseSensitivitySlider.value = savedSensitivity;
        MouseSensitivitySlider(); // Apply the loaded sensitivity
    }


}
