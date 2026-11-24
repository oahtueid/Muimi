using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using Ink.Runtime;
using System;
[System.Serializable]
public class SaveData
{
    public string currentStoryJson;
    public DateTime saveTime;
}

public static class BinarySaveSystem
{
    public static SaveData[] saveDatas {get; private set;}
    static string path = System.IO.Path.Combine(Application.persistentDataPath, "game_save.dat");


    public static void Save(SaveData data, int slot)
    {
        saveDatas[slot] = data;
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, saveDatas);

        stream.Close();

        Debug.Log("Save file at path: " + path);
    }

    public static SaveData Load(int slot)
    {
        if(saveDatas != null)
        {
            return saveDatas[slot];
        }
        else if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                SaveData[] data = formatter.Deserialize(stream) as SaveData[];
                stream.Close();
                return data[slot];
            }
            catch
            {
                Debug.LogWarning("Failed to load save data. The file may be corrupted. Creating a new save data.");
                stream.Close();
                return new SaveData();
            }
        }
        else
        {
            Debug.LogError("Save file does not exist at path: " + path + ". Creating new save file with default data.");
            SaveData defaultData = CreateDefaultSaveData();
            return defaultData;
        }
    }
    public static void Load()
    {
        if (saveDatas == null && File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            try
            {
                SaveData[] data = formatter.Deserialize(stream) as SaveData[];
                stream.Close();
                saveDatas = data;
            }
            catch
            {
                Debug.LogWarning("Failed to load save data. The file may be corrupted. Creating a new save data.");
                stream.Close();
            }
        }
        else if(!File.Exists(path))
        {
            Debug.LogError("Save file does not exist at path: " + path);
        }
    }
    private static SaveData CreateDefaultSaveData()
    {
        SaveData defaultData = new SaveData();
        TextAsset inkJson = Resources.Load<TextAsset>($"Dialogue/main");
        defaultData.currentStoryJson = "";
        defaultData.saveTime = DateTime.Now;
        return defaultData;
    }

}
