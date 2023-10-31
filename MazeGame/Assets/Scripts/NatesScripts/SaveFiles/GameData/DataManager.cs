using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    private DataStuff DataStuff;

    private List<IDataStuff> dataObjects;


    /// <summary>
    /// To load anything using a button use the line ---------> DataManager.instance."Function that needs to be prefromed"();
    /// </summary>

    public static DataManager instance { get; private set; }

    private void Awake()
    {

        

        if (instance != null)
        {
            Debug.LogError("Found more than one Data Manager in the scene");

        }
        instance = this;
    }

    private void Start()
    {
       // this.dataObjects = dataObjects();
        loadGame();

    }
    public void newGame()
    {
        this.DataStuff = new DataStuff();
    }

    public void loadGame()
    {
        // load ant saved data from a file using data handler 
        // if no data can be loaded, initilize to a new game
        if (this.gameObject == null)
        {
            Debug.Log("No data was found. initializing data to defaults.");
            newGame();
        }
    }

    public void saveGame()
    {
        // pass the data to other scripts so they can update it 
        // 
    }

    private void OnApplicationQuit()
    {
        saveGame();

    }

  //  private List<IDataStuff> dataObjects()
   // {
     //   IEnumerable<IDataStuff> dataObjects = 
   // }




}
