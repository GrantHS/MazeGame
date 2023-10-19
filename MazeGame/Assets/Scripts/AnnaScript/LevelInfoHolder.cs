using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoHolder : MonoBehaviour
{
    public LevelSelectInfo levelInfo;
    [SerializeField] private TextMeshProUGUI ratingTextPlaceholder;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject farmerSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        ratingTextPlaceholder.text = "Rating: " + levelInfo.rating.ToString();
        sizeText.text = levelInfo.levelSize.ToString();
    }

    public void OnLevelSelected()
    {
        FindAnyObjectByType<LevelSelectMenu>().selectedLevelNameText.text = levelInfo.levelName;
        FindAnyObjectByType<LevelSelectMenu>().selectedFarmerSpawn = farmerSpawn;
        FindAnyObjectByType<LevelSelectMenu>().selectedPlayerSpawn = playerSpawn;
    }

}
