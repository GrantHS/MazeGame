using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class DataStuff
{
    public float GameTime;

    // the values defined in this constructor will be default values 
    //the game starts with when theres no data to load 

    public DataStuff()
    {
        this.GameTime = 0;
    }


}
