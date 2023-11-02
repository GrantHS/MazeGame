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
    [SerializeField] private int indexNumber;

    //Level Objects
    [SerializeField] private GameObject levelObject;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject farmerSpawn;
    [SerializeField] private GameObject yellowKey;
    [SerializeField] private GameObject redKey;
    [SerializeField] private GameObject orangeKey;
    
    // Start is called before the first frame update
    void Start()
    {
        ratingTextPlaceholder.text = "Rating: " + levelInfo.rating.ToString();
        sizeText.text = levelInfo.levelSize.ToString();
    }

    public void OnLevelSelected()
    {
        FindAnyObjectByType<LevelSelectMenu>().selectedLevelIndex = indexNumber;
        FindAnyObjectByType<LevelSelectMenu>().selectedLevelNameText.text = levelInfo.levelName;
        FindAnyObjectByType<LevelSelectMenu>().selectedMazeLevel = levelObject;
        FindAnyObjectByType<LevelSelectMenu>().selectedFarmerSpawn = farmerSpawn;
        FindAnyObjectByType<LevelSelectMenu>().selectedPlayerSpawn = playerSpawn;

        FindAnyObjectByType<LevelSelectMenu>().selectedYellowKey = yellowKey;
        FindAnyObjectByType<LevelSelectMenu>().selectedRedKey = redKey;
        FindAnyObjectByType<LevelSelectMenu>().selectedOrangeKey = orangeKey;
    }

}
