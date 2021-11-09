using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SaveUID(Guid guid)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.agf";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(guid);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadUID()
    {
        string path = Application.persistentDataPath + "/player.agf";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData returnedData =  formatter.Deserialize(stream) as PlayerData;

            stream.Close();
            return returnedData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}
