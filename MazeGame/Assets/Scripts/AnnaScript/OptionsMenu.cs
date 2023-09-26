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
    [SerializeField, Range(0f,100f)] private float mouseSensitivity;

    private void Awake()
    {
        mouseSensitivity = FindAnyObjectByType<MouseLook>().mouseSen;
        sensitivityText.text = mouseSensitivity.ToString();
    }

    public void HUDOn()
    {
        gameplayHUD.SetActive(true);
        gameplayHUD.GetComponent<Canvas>().sortingOrder = this.GetComponent<Canvas>().sortingOrder - 2; //number probably subject to change
    }
    public void HUDOff() => gameplayHUD.SetActive(false);

    public void MouseSensitivitySlider()
    {

    }
}
