using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components
    private PlayerPhysics physics;

    // Status
    public bool isAir{
        get; private set;
    } = false;

    // Move
    private Vector2 moveInput = Vector2.zero;

    // Camera
    public CameraController cam;

    void Start()
    {
        // Register callbacks
        InputManager.instance.RegisterCallback(InputMap.InGame,"Vertical",OnVertical);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Horizontal",OnHorizontal);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Jump",OnJump);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Hold", OnHold);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Throw", OnThrow);
        InputManager.instance.RegisterCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Touch", OnTouch);

        // Components
        physics = GetComponent<PlayerPhysics>();

    }

    void Update()
    {
        if(moveInput != Vector2.zero){
            Vector3 moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
            moveDir.Normalize();
            physics.SetMoveDirection(moveDir);
        }
    }

    private void OnDestroy() {
        // Remove callbacks
        if(InputManager.instance != null){
            InputManager.instance.RemoveCallback(InputMap.InGame,"Vertical",OnVertical);
            InputManager.instance.RemoveCallback(InputMap.InGame,"Horizontal",OnHorizontal);
            InputManager.instance.RemoveCallback(InputMap.InGame,"Jump",OnJump);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Hold", OnHold);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Throw", OnThrow);
            InputManager.instance.RemoveCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Touch", OnTouch);
        }
    }
    public void OnVertical(InputAction.CallbackContext context){
        if(context.started){
            moveInput.y = context.ReadValue<float>();
        }
        else if(context.canceled){
            moveInput.y = 0;
            if(moveInput.x == 0){
                physics.SetMoveDirection(Vector3.zero);
            }
        }
    }
    public void OnHorizontal(InputAction.CallbackContext context){
        if(context.started){
            moveInput.x = context.ReadValue<float>();
        }
        if(context.canceled){
            moveInput.x = 0;
            if(moveInput.y == 0){
                physics.SetMoveDirection(Vector3.zero);
            }
        }
    }
    public void OnJump(InputAction.CallbackContext context){

    }
    public void OnHold(InputAction.CallbackContext context){

    }
    public void OnThrow(InputAction.CallbackContext context){

    }
    void OnViewRotate(InputAction.CallbackContext context){
        if(context.performed){
            Vector2 value = context.ReadValue<Vector2>();
            float angleY = transform.eulerAngles.y;
            angleY += value.x * 0.3f;
            transform.rotation = Quaternion.Euler(0, angleY, 0);
            cam.UpdateRotation(-value.y * 0.3f);
        }
    }
    public void OnPush(InputAction.CallbackContext context){
        
    }
    public void OnTouch(InputAction.CallbackContext context){
        var touch = Touchscreen.current.primaryTouch;

            if (touch.press.isPressed && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Debug.Log($"터치 시작: {touch.position.ReadValue()}");
            }
            // else if (touch.press.isPressed && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            // {
            //     Debug.Log($"터치 중: {touch.position.ReadValue()}");
            // }
            else if (!touch.press.isPressed && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                Debug.Log("터치 종료");
            }
        
    }
}
