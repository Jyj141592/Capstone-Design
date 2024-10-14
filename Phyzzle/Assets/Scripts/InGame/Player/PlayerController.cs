using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Components
    private Animator animator;
    private Rigidbody rigid;
    
    // Status
    public bool isMovable{
        get => animator.GetCurrentAnimatorStateInfo(0).tagHash != haltHash;
    }
    private Vector2 move = Vector2.zero;
    private float maxSpeed = 10.0f;
    private bool isAir = false;
    private Vector3 up = Vector3.up;
    public bool inverted{
        get; private set;
    } = false;
    private Vector3 moveDir = Vector3.zero;
    private bool isHold = false;
    private bool isPush = false;

    // Hashs
    private int haltHash;
    private int moveHash;
    private int isAirHash;
    private int jumpHash;
    private int holdHash;
    private int stackHash;
    private int pushHash;
    private int throwHash;

    // Camera
    private Vector3 cameraPos = Vector3.zero;
    private Vector3 cameraLook = Vector3.forward;

    // Objects
    public GameObject handPos;
    private BoxCollider handCol;
    public Transform hand;
    private Collider hold = null;
    void Start()
    {
        // Register callbacks
        InputManager.instance.RegisterCallback(InputMap.InGame,"Vertical",OnVertical);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Horizontal",OnHorizontal);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Jump",OnJump);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Hold", OnHold);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Throw", OnThrow);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Touch", OnTouch);

        // Components
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        // Hashs
        haltHash = Animator.StringToHash("halt");
        moveHash = Animator.StringToHash("move");
        isAirHash = Animator.StringToHash("isAir");
        jumpHash = Animator.StringToHash("jump");
        holdHash = Animator.StringToHash("hold");
        stackHash = Animator.StringToHash("stack");
        pushHash = Animator.StringToHash("push");
        throwHash = Animator.StringToHash("throw");

        handCol = handPos.GetComponent<BoxCollider>();
    }

    private void Update() {
        if(isMovable && (move.x != 0 || move.y != 0)){
            Vector3 forward = transform.position - cameraPos;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = Vector3.Cross(up, forward);
            Vector3 m = forward * move.y + right * move.x;
            m.Normalize();

            if(isPush){
                
            }
            else{
                
                transform.forward = m;

                if(m != moveDir){
                    float l = Vector3.Dot(rigid.velocity, moveDir);
                    if(l < maxSpeed){
                        rigid.velocity = rigid.velocity - moveDir * l;
                    }
                    else{
                        rigid.velocity = rigid.velocity - moveDir * maxSpeed;
                    }
                }


                float len = Vector3.Dot(rigid.velocity, m);
                if(len < maxSpeed){
                    rigid.velocity += (maxSpeed - len) * m;
                }
                moveDir = m;
            }
        }
    }


    private void OnCollisionEnter(Collision other) {
        // int mask = LayerMask.NameToLayer("Ground") | LayerMask.NameToLayer("Cube");
        // if((other.gameObject.layer & mask) > 0){
            isAir = false;
            animator.SetBool(isAirHash, false);
        //}
    }

    private void OnDestroy() {
        // Remove callbacks
        if(InputManager.instance != null){
            InputManager.instance.RemoveCallback(InputMap.InGame,"Vertical",OnVertical);
            InputManager.instance.RemoveCallback(InputMap.InGame,"Horizontal",OnHorizontal);
            InputManager.instance.RemoveCallback(InputMap.InGame,"Jump",OnJump);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Hold", OnHold);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Throw", OnThrow);
            InputManager.instance.RemoveCallback(InputMap.InGame, "Touch", OnTouch);
        }
    }

    public void SetHold(){
        hold.attachedRigidbody.isKinematic = true;
        hold.gameObject.transform.SetParent(hand);
        hold.gameObject.transform.localPosition = Vector3.zero;
        hold.gameObject.transform.localRotation = Quaternion.identity;
        hold.enabled = false;
    }
    public void ClearHold(){
        hold.enabled = true;
        hold.gameObject.transform.SetParent(null);
        hold.attachedRigidbody.isKinematic = false;
        hold = null;
    }
    public void Throw(){
        hold.enabled = true;
        hold.gameObject.transform.SetParent(null);
        hold.attachedRigidbody.isKinematic = false;
        hold.attachedRigidbody.AddForce((transform.forward + new Vector3(0,0.9f,0)) * 800);
    }

    public void SetCameraPosition(Vector3 cameraPos, Vector3 cameraLook){
        this.cameraPos = cameraPos;
        this.cameraLook = cameraLook;
    }

    public void OnVertical(InputAction.CallbackContext context){
        if(context.started){
            move.y = context.ReadValue<float>();
            animator.SetBool(moveHash, true);
        }
        else if(context.canceled){
            move.y = 0;
            
            if(move.x == 0){
                animator.SetBool(moveHash, false);
                float len = Vector3.Dot(transform.forward, rigid.velocity);
                rigid.velocity = rigid.velocity - transform.forward * len;
            }
        }
    }
    public void OnHorizontal(InputAction.CallbackContext context){
        if(context.started){
            move.x = context.ReadValue<float>();
            animator.SetBool(moveHash, true);
        }
        else if(context.canceled){
            move.x = 0;
            
            if(move.y == 0){
                animator.SetBool(moveHash, false);
                float len = Vector3.Dot(transform.forward, rigid.velocity);
                rigid.velocity = rigid.velocity - transform.forward * len;
            }
        }
    }
    public void OnJump(InputAction.CallbackContext context){
        if(context.started && !isAir){
            animator.SetTrigger(jumpHash);
            animator.SetBool(isAirHash, true);
            isAir = true;
            rigid.AddForce(Vector3.up * 1000);
        }
    }
    public void OnHold(InputAction.CallbackContext context){
        if(isMovable && context.started){
            //animator.SetBool(holdHash, !animator.GetBool(holdHash));
            if(isHold){
                // hold.gameObject.transform.SetParent(null);
                // hold.attachedRigidbody.isKinematic = false;
                // hold = null;
                var colls = Physics.OverlapSphere(handPos.transform.position, 0.7f, ~LayerMask.GetMask("Player"));
                if(colls.Length > 0){
                    animator.SetBool(stackHash, true);
                    ClearHold();
                }
                else{
                    animator.SetBool(stackHash, false);
                }
                isHold = false;
                animator.SetBool(holdHash, false);
                handCol.enabled = false;
            }
            else if(isPush){
                isPush = false;
                hold = null;
                animator.SetBool(pushHash, false);
                handCol.enabled = false;
            }
            else{
                var colls = Physics.OverlapSphere(handPos.transform.position, 0.7f, LayerMask.GetMask("Cube"));
                if(colls.Length > 0){
                    isHold = true;
                    animator.SetBool(holdHash, true);
                    hold = colls[0];
                    handCol.enabled = true;
                    handCol.center = new Vector3(0,1.5f, -1.0f);
                    // colls[0].attachedRigidbody.isKinematic = true;
                    // colls[0].gameObject.transform.SetParent(hand.transform);
                    // colls[0].gameObject.transform.localPosition = Vector3.zero;
                }
                else{
                    colls = Physics.OverlapSphere(handPos.transform.position, 0.7f, LayerMask.GetMask("Pushable"));
                    if(colls.Length > 0){
                        isPush = true;
                        animator.SetBool(pushHash, true);
                        hold = colls[0];
                        handCol.enabled = true;
                        handCol.center = new Vector3(0, 1.5f, -2.0f);
                    }
                }
            }
            
        }
    }
    public void OnThrow(InputAction.CallbackContext context){
        if(isMovable && isHold && context.started){
            isHold = false;
            animator.SetTrigger(throwHash);
            animator.SetBool(holdHash, false);
        }
    }
    public void OnPush(InputAction.CallbackContext context){
        
    }
    public void OnTouch(InputAction.CallbackContext context){
        var touch = Touchscreen.current.primaryTouch;

            if (touch.press.isPressed && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
            {
                // 터치가 처음 시작될 때
                Debug.Log($"터치 시작: {touch.position.ReadValue()}");
            }
            // else if (touch.press.isPressed && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved)
            // {
            //     // 터치가 움직이는 중
            //     Debug.Log($"터치 중: {touch.position.ReadValue()}");
            // }
            else if (!touch.press.isPressed && touch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
            {
                // 터치가 종료될 때
                Debug.Log("터치 종료");
            }
        
    }
}
