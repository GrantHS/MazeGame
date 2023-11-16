using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoHolder : MonoBehaviour
{
    public LevelSelectInfo levelInfo;
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
        sizeText.text = levelInfo.levelSize.ToString();
    }

    public void OnLevelSelected()
    {
        FindAnyObjectByType<LevelSelectMenu>().selectedLevelIndex = indexNumber;
        FindAnyObjectByType<LevelSelectMenu>().selectedLevelNameText.text = levelInfo.levelName;

        GameManager.Instance.currentLevel = levelObject;
        GameManager.Instance.playerSpawn = playerSpawn;
        GameManager.Instance.farmerSpawn = farmerSpawn;
        GameManager.Instance.yellowKey = yellowKey;
        GameManager.Instance.redKey = redKey;
        GameManager.Instance.orangeKey = orangeKey;
    }

}
