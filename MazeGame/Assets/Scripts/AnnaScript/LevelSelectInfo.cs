using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSelectInfo", menuName = "ScriptableObjects/LevelSelectInfoScriptableObjects", order = 1)]
public class LevelSelectInfo : ScriptableObject
{
    public string levelName;
    public float rating;
    public string levelSize;
    public float savedTime;
    public GameObject mazeLevelObject;
}
