using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewUI : InputUI, IDragHandler, IBeginDragHandler
{
    private Vector2 prevPosition = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        prevPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        controller.ViewRotate(eventData.position - prevPosition);
        prevPosition = eventData.position;
    }
}
