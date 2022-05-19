using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(HighScoreData newData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/SaveData" + newData.levelName + ".txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        //HighScoreData data = new HighScoreData(SceneManager.GetActiveScene().name);

        formatter.Serialize(stream, newData);

        stream.Close();

    }

    public static HighScoreData LoadPlayer()
    {
        string levelName = SceneManager.GetActiveScene().name;

        string path = Application.persistentDataPath + "/SaveData" + levelName + ".txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            HighScoreData data = formatter.Deserialize(stream) as HighScoreData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogWarning("No file data found");
            return new HighScoreData(levelName);
        }

    }

}
