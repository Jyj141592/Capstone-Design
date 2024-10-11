using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{
    private string savePath = Path.Combine(Application.persistentDataPath, "savefile");
    private const string savefileName = "save.json";
    private string customMapPath = Path.Combine(Application.persistentDataPath, "maps");
    
    public GameData LoadGameData(){
        if(!Directory.Exists(savePath)){
            Directory.CreateDirectory(savePath);
        }
        string filePath = Path.Combine(savePath, savefileName);
        if(File.Exists(filePath)){
            string json = File.ReadAllText(savePath);
            GameData data = JsonUtility.FromJson<GameData>(json);
            return data;
        }
        else{
            GameData data = new GameData();
            SaveGameData(data);
            return data;
        }
    }
    public void SaveGameData(GameData data){
        if(!Directory.Exists(savePath)){
            Directory.CreateDirectory(savePath);
        }
        string filePath = Path.Combine(savePath, savefileName);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(filePath, json);
    }

}
