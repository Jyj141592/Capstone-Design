using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonController : MonoBehaviour
{
    private IButtonAction buttonAction;
    public Transform button;
    private float originalPos = 0.0f;
    private float pressedPos = -0.3f;
    private bool pressed = false;
    private bool isTweening = false;
    private Tween tween = null;
    private int contacted = 0;
    private void OnTriggerEnter(Collider other) {
        if(contacted == 0){
            if(isTweening){
                tween.Kill();
                tween = null;
            }
            isTweening = true;
            tween = button.DOLocalMoveY(pressedPos, button.localPosition.y - pressedPos).OnComplete(() => {
                //pressed = true;
                isTweening = false;
                if(!pressed){
                    buttonAction.OnPressed();
                    pressed = true;
                }
            });
        }
        contacted++;
    }
    private void OnTriggerExit(Collider other) {
        contacted--;
        if(contacted == 0){
            if(isTweening){
                tween.Kill();
                tween = null;
            }
            if(pressed){
                buttonAction.OnReleased();
                pressed = false;
            }
            isTweening = true;
            //pressed = false;
            tween = button.DOLocalMoveY(originalPos, originalPos - button.localPosition.y).OnComplete(() => {
                isTweening = false;
            });
        }
    }
    private void Start() {
        buttonAction = GetComponent<IButtonAction>();
    }
}
