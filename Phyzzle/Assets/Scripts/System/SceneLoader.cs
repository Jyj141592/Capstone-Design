using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    
    public override void Init()
    {
        
    }
    public void LoadSceneAsync(string scene, bool activateOnLoad){
        var handle = Addressables.LoadSceneAsync(scene, LoadSceneMode.Single, activateOnLoad);
    }
}
