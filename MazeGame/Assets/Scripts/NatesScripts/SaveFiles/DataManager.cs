using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private DataStuff dataStuff;

    private List<IDataStuff> dataPersistenceObjects;
    private FileDataHandler dataHandler;


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
         this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
         this.dataPersistenceObjects = FindAllDataPersistenceObjects();
         loadGame();

    }
    public void newGame()
    {
        this.dataStuff = new DataStuff();
        Debug.Log("New Game Ran");
    }

    public void loadGame()
    {

        this.dataStuff = dataHandler.Load();


        if (this.dataStuff == null)
        {
            Debug.Log("Loading....... ");
            newGame();
        }
       

        foreach (IDataStuff dataObject in dataPersistenceObjects)
        {
            dataObject.LoadData(dataStuff);
           
        }

        Debug.Log("Load Mouse sens is: " + dataStuff.MouseSens);
        Debug.Log("Load Time is: " + dataStuff.GameTime);
    }

    public void saveGame()
    {
      
        foreach (IDataStuff dataObject in dataPersistenceObjects)
        {
            dataObject.SaveData(ref dataStuff);
            
        }

        Debug.Log("Save Mouse sens is: " + dataStuff.MouseSens);
        Debug.Log("Save Time is: " + dataStuff.GameTime);
        dataHandler.Save(dataStuff);

    }


    private void OnApplicationQuit()
    {
        saveGame();

    }

    private List<IDataStuff> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataStuff> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataStuff>();


        return new List<IDataStuff>(dataPersistenceObjects);
    }



}

