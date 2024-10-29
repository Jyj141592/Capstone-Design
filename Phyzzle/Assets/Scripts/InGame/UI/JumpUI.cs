using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUI : InputUI
{
    public void OnClicked(){
        controller.Jump();
    }
}
