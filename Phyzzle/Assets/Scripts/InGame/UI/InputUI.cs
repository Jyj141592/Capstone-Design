using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputUI : MonoBehaviour
{
    protected PlayerController controller;

    public virtual void SetPlayer(PlayerController playerController){
        controller = playerController;
    }
}
