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
    private bool _isAir = false;
    public bool isAir{
        get => _isAir;
        private set {
            if(value != _isAir){
                if(value && moveInput != Vector2.zero){
                    cam.StopWalk();
                }
                else if(!value && moveInput != Vector2.zero){
                    cam.Walk();
                }
                _isAir = value;
            }
        }
    }
    private HoldObject hold;
    // private bool isHold = false;


    // Move
    private Vector2 moveInput = Vector2.zero;

    // Camera
    private CameraController cam;

    // Bow
    private BowController bow;

    void Start()
    {
        // Register callbacks
        InputManager.instance.RegisterCallback(InputMap.InGame,"Vertical",OnVertical);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Horizontal",OnHorizontal);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Jump",OnJump);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Hold", OnHold);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Throw", OnThrow);
        InputManager.instance.RegisterCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Bow", OnBow);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Shoot", OnShoot);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Touch", OnTouch);

        // Components
        physics = GetComponent<PlayerPhysics>();
        cam = GetComponentInChildren<CameraController>();
        bow = GetComponentInChildren<BowController>(true);

        // Hold
        hold = GetComponentInChildren<HoldObject>();
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
            InputManager.instance.RemoveCallback(InputMap.InGame, "Bow", OnBow);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Shoot", OnShoot);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Touch", OnTouch);
        }
    }
    public void OnVertical(InputAction.CallbackContext context){
        if(context.started){
            if(moveInput == Vector2.zero) cam.Walk();
            moveInput.y = context.ReadValue<float>();
        }
        else if(context.canceled){
            moveInput.y = 0;
            if(moveInput.x == 0){
                physics.SetMoveDirection(Vector3.zero);
                cam.StopWalk();
            }
        }
    }
    public void OnHorizontal(InputAction.CallbackContext context){
        if(context.started){
            if(moveInput == Vector2.zero){
                cam.Walk();
            } 
            moveInput.x = context.ReadValue<float>();
        }
        if(context.canceled){
            moveInput.x = 0;
            if(moveInput.y == 0){
                physics.SetMoveDirection(Vector3.zero);
                cam.StopWalk();
            }
        }
    }
    public void OnJump(InputAction.CallbackContext context){
        if(context.started && !isAir){
            physics.AddForce(Vector3.up * 1000);
        }
    }
    public void OnHold(InputAction.CallbackContext context){
        if(context.started){
            hold.Hold();
        }
    }
    public void OnThrow(InputAction.CallbackContext context){
        if(context.started){
            hold.Throw();
        }
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
    public void OnBow(InputAction.CallbackContext context){
        if(context.started){
            bow.StartTarget();
        }
        else if(context.canceled){
            bow.StopTarget();
        }
    }
    public void OnShoot(InputAction.CallbackContext context){
        if(context.started){
            bow.Shoot();
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
