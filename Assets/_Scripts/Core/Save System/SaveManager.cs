using System;
using System.IO;
//using GRFON;
using NaughtyAttributes;
using UnityEngine;

public static class SaveManager
{
    public static void SaveData(string name, int value) => PlayerPrefs.SetInt(name, value);

    public static void SaveData(string name, float value) => PlayerPrefs.SetFloat(name, value);

    public static void SaveData(string name, string value) => PlayerPrefs.SetString(name, value);

    public static void SaveData(string name, bool value) => PlayerPrefs.SetString(name, value.ToString());

    /*public static void SaveData(GrfonCollection grfonCollection, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        using Stream stream = new FileStream(filePath, FileMode.Create);
        using GrfonStreamOutput output = new GrfonStreamOutput(stream);
        
        grfonCollection.SerializeTo(output);
    }*/

    public static void GetData(string name, out int resultValue)
    {
        var isDataAvailable = PlayerPrefs.HasKey(name);
        
        if (isDataAvailable)
        {
            resultValue = PlayerPrefs.GetInt(name);
        }
        else
        {
            PlayerPrefs.SetInt(name, 0);
            resultValue = 0;
        }
    }

    public static void GetData(string name, out float resultValue)
    {
        var isDataAvailable = PlayerPrefs.HasKey(name);

        if (isDataAvailable)
        {
            resultValue = PlayerPrefs.GetFloat(name);
        }
        else
        {
            PlayerPrefs.SetFloat(name, 0);
            resultValue = 0;
        }
    }

    public static void GetData(string name, out string resultValue)
    {
        var isDataAvailable = PlayerPrefs.HasKey(name);

        if (isDataAvailable)
        {
            resultValue = PlayerPrefs.GetString(name);
        }
        else
        {
            PlayerPrefs.SetString(name, String.Empty);
            resultValue = String.Empty;
        }
    }

    public static void GetData(string name, out bool resultValue)
    {
        resultValue = false;

        var isDataAvailable = PlayerPrefs.HasKey(name);

        if (!isDataAvailable) return;
        
        var value = PlayerPrefs.GetString(name);
        var isCorrectFormat = bool.TryParse(value, out resultValue);

        if (!isCorrectFormat)
        {
            resultValue = false;
        }
    }
    
    /*public static void GetData(string fileName, out GrfonCollection collection)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        
        using (Stream stream = new FileStream(filePath, FileMode.OpenOrCreate)) 
        {
            GrfonDeserializer des = new GrfonDeserializer(new GrfonStreamInput(stream));
            collection = des.Parse() as GrfonCollection;
        } 
    }*/

    public static void DeleteData(string name) => PlayerPrefs.DeleteKey(name);

    public static void DeleteFileData(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        File.Delete(filePath);
    }

    public static bool HasData(string name) => PlayerPrefs.HasKey(name);
}
