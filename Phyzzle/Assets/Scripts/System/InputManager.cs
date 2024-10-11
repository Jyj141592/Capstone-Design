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
    private InputMap currentActionMap = InputMap.InGame;
    public override void Init()
    {
        var handle = Addressables.LoadAssetAsync<InputActionAsset>("InputAsset");
        handle.WaitForCompletion();
        if(handle.Status == AsyncOperationStatus.Succeeded){
            inputActions = handle.Result;
            inputActions.FindActionMap("InGame").Enable();
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

    public void ChangeActionMap(InputMap map){
        if(map == currentActionMap){
            return;
        }
        switch(map){
            case InputMap.UI:
                inputActions.FindActionMap("UI").Enable();
                break;
            case InputMap.InGame:
                inputActions.FindActionMap("InGame").Enable();
                break;
            case InputMap.Editor:
                inputActions.FindActionMap("Editor").Enable();
                break;
        }
        switch(currentActionMap){
            case InputMap.UI:
                inputActions.FindActionMap("UI").Disable();
                break;
            case InputMap.InGame:
                inputActions.FindActionMap("InGame").Disable();
                break;
            case InputMap.Editor:
                inputActions.FindActionMap("Editor").Disable();
                break;
        }
        currentActionMap = map;
    }
}
