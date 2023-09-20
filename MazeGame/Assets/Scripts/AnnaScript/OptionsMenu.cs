using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private UIMenu menu;
    [SerializeField] private GameObject gameplayHUD;
    // Start is called before the first frame update
    
    public void HUDOn()
    {
        gameplayHUD.SetActive(true);
        gameplayHUD.GetComponent<Canvas>().sortingOrder = this.GetComponent<Canvas>().sortingOrder - 2; //number probably subject to change
    }
    public void HUDOff() => gameplayHUD.SetActive(false);


}
