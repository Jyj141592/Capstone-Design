using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    // Save & Load
    private SaveManager saveManager = new SaveManager();

    // Game data
    private GameData data;

    public override void Init()
    {
        
    }

    public void InitGame(){
        data = saveManager.LoadGameData();
    }
}
