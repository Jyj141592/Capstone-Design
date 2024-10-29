using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowUI : InputUI, IPointerUpHandler, IPointerDownHandler
{
    // public void OnDrag(PointerEventData eventData)
    // {
        
    // }

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.Bow(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        controller.Shoot();
        controller.Bow(false);
    }
}
