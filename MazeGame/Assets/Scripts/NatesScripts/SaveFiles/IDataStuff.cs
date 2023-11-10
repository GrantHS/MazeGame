using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataStuff
{
    void LoadData(DataStuff data);
    void SaveData(ref DataStuff data);

}
