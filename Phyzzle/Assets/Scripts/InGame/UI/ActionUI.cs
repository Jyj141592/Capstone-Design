using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionUI : InputUI
{
    private bool isHold = false;
    public GameObject throwButton;
    public GameObject shotButton;
    public override void SetPlayer(PlayerController playerController)
    {
        base.SetPlayer(playerController);
        playerController.GetComponentInChildren<HoldObject>().onHoldChanged += OnHoldChanged;
    }
    public void OnHoldChanged(bool hold){
        isHold = hold;
        if(isHold){
            throwButton.SetActive(true);
            shotButton.SetActive(false);
        }
        else{
            throwButton.SetActive(false);
            shotButton.SetActive(true);
        }
    }
    public void OnThrow(){
        controller.Throw();
    }
}
