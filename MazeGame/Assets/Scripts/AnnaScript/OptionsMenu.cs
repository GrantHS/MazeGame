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
    [SerializeField] private TextMeshProUGUI resolutionCheckText;
    [SerializeField] private int resolutionMultiplier;
    [SerializeField] private Slider resolutionSlider;
    [SerializeField] private TextMeshProUGUI resolutionMultiplierText;
    public GameObject MiniMap_Heater;

    public float mouseSensitivity;

    private void Awake()
    {
        LoadFromStorage();

        //mouseSensitivitySlider.value = FindAnyObjectByType<MouseLook>().mouseSen / 10;
        sensitivityText.text = mouseSensitivitySlider.value.ToString();
        resolutionMultiplierText.text = resolutionSlider.value.ToString();

    }

    //Save Game Stuff

  
    private void OnEnable()
    {
       // MiniMap_Heater.SetActive(true);
       GameManager.Instance.currentMenuOpened = UIMenu.Options;
       GameManager.Instance.countingTime = !GameManager.Instance.countingTime;
       Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        resolutionCheckText.text = "Current Screen Resolution is: " + Screen.width + "x" + Screen.height;
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

    private int GetDivisor(int x, int y)
    {
        int d;
        d = GCD(x, y);
        
        Debug.Log("Divisor is:" + d);
        
        return d;
    }

    static int GCD(int a, int b)
    {
        if (b == 0)
            return a;
        return GCD(b, a % b);
    }

    public void FullscreenToggle()
    {
        int screenWidth = Display.main.systemWidth;
        int screenHeight = Display.main.systemHeight;

        int divisor = GetDivisor(screenWidth, screenHeight);

        screenWidth /= divisor;
        screenHeight /= divisor;
        Debug.Log("Aspect Ratio of Monitor is: " + screenWidth + ":" + screenHeight);

        screenWidth *= resolutionMultiplier;
        screenHeight *= resolutionMultiplier;
        Debug.Log("Current Screen Resolution is: " + screenWidth + "x" + screenHeight);

        Screen.SetResolution(screenWidth, screenHeight, true);
    }

    public void WindowedToggle()
    {
        int screenWidth = Display.main.systemWidth;
        int screenHeight = Display.main.systemHeight;

        int divisor = GetDivisor(screenWidth, screenHeight);

        screenWidth /= divisor;
        screenHeight /= divisor;
        Debug.Log("Aspect Ratio of Monitor is: " + screenWidth + ":" + screenHeight);

        screenWidth *= resolutionMultiplier;
        screenHeight *= resolutionMultiplier;
        Debug.Log("Current Screen Resolution is: " + screenWidth + "x" + screenHeight);

        Screen.SetResolution(screenWidth, screenHeight, false);
    }

    public void ResolutionMultiplierSlider()
    {
        resolutionMultiplier = (int)resolutionSlider.value;
        resolutionMultiplierText.text = resolutionSlider.value.ToString();
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
