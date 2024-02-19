using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class SaveManager 
{
    public static string fileName="data.json";
    public static void SaveData(object data)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        string jsonData = JsonConvert.SerializeObject(data);
        File.WriteAllText(filePath, jsonData);
    }

    public static bool isFileExists()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath)) return true;
        else return false;
    }

    public static T LoadData<T>()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName) ;
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            T loadedObject = JsonConvert.DeserializeObject<T>(jsonData);
            return loadedObject;
        }
        else
        {
            string jsonData = JsonConvert.SerializeObject(new PlayerStats
            {
                PlayerName="Appilab",
                nbrOfCoins=0,
                Money=0,
                MaxBallExechange=0
            });
            File.WriteAllText(filePath, jsonData);
            return default(T);
        }
    }
}
