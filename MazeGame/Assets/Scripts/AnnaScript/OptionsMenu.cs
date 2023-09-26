using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private UIMenu menu;
    [SerializeField] private GameObject gameplayHUD;
    [SerializeField] private Slider mouseSensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;

    private void Awake()
    {
        FindAnyObjectByType<MouseLook>().mouseSen = mouseSensitivitySlider.value * 10;
        sensitivityText.text = mouseSensitivitySlider.value.ToString();
    }

    public void HUDOn()
    {
        gameplayHUD.SetActive(true);
        gameplayHUD.GetComponent<Canvas>().sortingOrder = this.GetComponent<Canvas>().sortingOrder - 2; //number probably subject to change
    }
    public void HUDOff() => gameplayHUD.SetActive(false);

    public void MouseSensitivitySlider(float mouseSensitivity)
    {
        mouseSensitivity = mouseSensitivitySlider.value * 10;
        FindAnyObjectByType<MouseLook>().mouseSen = mouseSensitivity;
        sensitivityText.text = mouseSensitivitySlider.value.ToString();
    }
}
