using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
   
    public static void Save(Initialization dataGame)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/game.sav";
        FileStream stream = new FileStream(path, FileMode.Create);

        DataGame data = new DataGame(dataGame);
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataGame Load()
    {
        string path = Application.persistentDataPath + "/game.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            DataGame data = formatter.Deserialize(stream) as DataGame;
            stream.Close();

            return data;
        }
        else {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }

}
