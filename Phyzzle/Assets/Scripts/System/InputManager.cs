using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum InputMap {
    UI, InGame, Editor
}
public class InputManager : Singleton<InputManager>
{
    private InputActionAsset inputActions;
    public override void Init()
    {
        var handle = Addressables.LoadAssetAsync<InputActionAsset>("InputAsset");
        handle.WaitForCompletion();
        if(handle.Status == AsyncOperationStatus.Succeeded){
            inputActions = handle.Result;
        }
        else{
            // Error
        }
    }
    public void RegisterCallback(InputMap map, string action, Action<InputAction.CallbackContext> callback){
        InputAction a = null;
        switch(map){
            case InputMap.UI:
                a = inputActions.FindActionMap("UI").FindAction(action);
                break;
            case InputMap.InGame:
                a = inputActions.FindActionMap("InGame").FindAction(action);
                break;
            case InputMap.Editor:
                a = inputActions.FindActionMap("Editor").FindAction(action);
                break;
        }
        if(a == null) throw new KeyNotFoundException($"The action '{action}' was not found in the InputActionMap.");
        a.started += callback;
        a.performed += callback;
        a.canceled += callback;
    }
    public void RemoveCallback(InputMap map, string action, Action<InputAction.CallbackContext> callback){
        InputAction a = null;
        switch(map){
            case InputMap.UI:
                a = inputActions.FindActionMap("UI").FindAction(action);
                break;
            case InputMap.InGame:
                a = inputActions.FindActionMap("InGame").FindAction(action);
                break;
            case InputMap.Editor:
                a = inputActions.FindActionMap("Editor").FindAction(action);
                break;
        }
        if(a != null){
            a.started -= callback;
            a.performed -= callback;
            a.canceled -= callback;
        }
    }
}
