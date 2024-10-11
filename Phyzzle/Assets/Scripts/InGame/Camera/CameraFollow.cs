using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    public float depth = 10.0f;
    public Vector2 offset = Vector2.zero;
    public float sensitive = 1.0f;
    private PlayerController controller;
    private float angle = 0.0f;
    void Start()
    {
        controller = follow.GetComponent<PlayerController>();
        UpdatePosition();
        InputManager.instance.RegisterCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
    }
    void Update()
    {
        UpdatePosition();
    }
    private void OnDestroy() {
        if(InputManager.instance != null){
            InputManager.instance.RemoveCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
        }
    }
    void UpdatePosition(){
        Vector3 pos = new Vector3(-Mathf.Sin(angle), 0, -Mathf.Cos(angle));
        transform.forward = -pos;
        pos *= depth;
        pos += transform.up * offset.y + transform.right * offset.x + follow.position;
        transform.position = pos;
        controller.SetCameraPosition(transform.position);
    }
    void OnViewRotate(InputAction.CallbackContext context){
        if(context.performed){
            Vector2 value = context.ReadValue<Vector2>();
            angle += value.x * 0.1f * sensitive;
        }
    }
}
