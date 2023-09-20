using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayHUD : MonoBehaviour
{
    [SerializeField] private GameObject redkeysprite;
    [SerializeField] private GameObject orangekeysprite;
    [SerializeField] private GameObject yellowkeysprite;



    private void Awake()
    {
        redkeysprite.SetActive(false);
        orangekeysprite.SetActive(false);
        yellowkeysprite.SetActive(false);
    }

    private void Update() //placeholder, this can probably be refined later
    {
        if(FindAnyObjectByType<PushDoor>().redKey == true)
        {
            redkeysprite.SetActive(true);
        }
        if(FindAnyObjectByType<PushDoor>().orangeKey == true)
        {
            orangekeysprite.SetActive(true);
        }
        if(FindAnyObjectByType<PushDoor>().yellowKey == true)
        {
            yellowkeysprite.SetActive(true);
        }
    }
}
