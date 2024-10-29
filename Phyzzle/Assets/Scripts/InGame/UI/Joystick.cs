using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Joystick : InputUI, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform handle;
    public RectTransform lever;
    private Vector2 originalCenter;

    private void Start() {
        handle = GetComponent<RectTransform>();
        originalCenter = handle.localPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {

        Vector2 direction = eventData.position - handle.anchoredPosition;
        //lever.anchoredPosition = direction;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            handle,
            eventData.position,
            null,
            out direction
        );
        direction = Vector2.ClampMagnitude(direction, 100f);
        lever.anchoredPosition = direction;

        controller.OnMove(InputCallback.performed, direction);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        controller.OnMove(InputCallback.started, Vector3.zero);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        controller.OnMove(InputCallback.canceled, Vector2.zero);
    }
}
