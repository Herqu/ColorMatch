using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEditor;

public static class SaveSystem 
{
    public static void SaveLevel(int currentLevel)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        CurrentLevelData data = new CurrentLevelData(currentLevel);
        formatter.Serialize(stream, data);
        stream.Close() ;
    }

    public static int LoadLevel()
    {
        string path = Application.persistentDataPath + "/player.fun";

        if (File.Exists(path))
        {
            //return 3;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            CurrentLevelData data = formatter.Deserialize(stream) as CurrentLevelData;
            stream.Close();
            return data.LevelNum;
        }
        else
        {
            Debug.Log("还没有开始，正在第一关");
            return 1;
        }
    }

    #if UNITY_EDITOR
    [MenuItem("GameControl/Reset")]
    public static void ResetGameLevel()
    {
        int currentLevel = 1;
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        CurrentLevelData data = new CurrentLevelData(currentLevel);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    #endif 

}
