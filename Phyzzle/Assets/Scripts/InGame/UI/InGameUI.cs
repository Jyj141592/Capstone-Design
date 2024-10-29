using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum InputCallback{
        started, performed, canceled
    }
public class InGameUI : MonoBehaviour
{
    private void Start() {
        GameObject player = GameObject.FindWithTag("Player");
        var buttons = GetComponentsInChildren<InputUI>();
        PlayerController controller = player.GetComponent<PlayerController>();
        for(int i = 0; i < buttons.Length; i++){
            buttons[i].SetPlayer(controller);
        }    
    }
    
}
