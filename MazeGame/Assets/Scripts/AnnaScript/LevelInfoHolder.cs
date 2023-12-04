using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoHolder : MonoBehaviour
{
    public LevelSelectInfo levelInfo;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private TextMeshProUGUI savedTimeText;
    [SerializeField] private int indexNumber;

    //Level Objects
    [SerializeField] private GameObject levelObject;
    [SerializeField] private GameObject playerSpawn;
    [SerializeField] private GameObject farmerSpawn;
    [SerializeField] private GameObject yellowKey;
    [SerializeField] private GameObject redKey;
    [SerializeField] private GameObject orangeKey;
    
    void OnEnable()
    {
        sizeText.text = levelInfo.levelSize.ToString();
        savedTimeText.text = "Best Time: " + TimeFixer();
    }


    private string TimeFixer()
    {
        int minutes = Mathf.FloorToInt(levelInfo.savedTime / 60f);
        int seconds = Mathf.FloorToInt(levelInfo.savedTime - minutes * 60f);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        return niceTime;
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
