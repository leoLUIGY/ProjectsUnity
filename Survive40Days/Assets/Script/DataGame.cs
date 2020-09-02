using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataGame 
{
    public int recordDays;
    public DataGame(Initialization data)
    {
        recordDays = data.recordDays;
    }
}
