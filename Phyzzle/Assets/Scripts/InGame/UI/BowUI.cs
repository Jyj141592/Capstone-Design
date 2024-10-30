using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowUI : InputUI, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    private Vector2 prevPosition = Vector2.zero;
    public override void SetPlayer(PlayerController playerController)
    {
        base.SetPlayer(playerController);
        playerController.GetComponentInChildren<BowController>(true).onAim += OnAim;
    }
    public GameObject aim;

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.Bow(true);
        prevPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controller.Shoot();
        controller.Bow(false);
    }
    public void OnAim(bool aim){
        this.aim.SetActive(aim);
    }

    public void OnDrag(PointerEventData eventData)
    {
        controller.ViewRotate(eventData.position - prevPosition);
        prevPosition = eventData.position;
    }
}
