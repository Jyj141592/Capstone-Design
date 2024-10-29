using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldUI : InputUI
{
    public void OnClicked(){
        controller.Hold();
    }
}
