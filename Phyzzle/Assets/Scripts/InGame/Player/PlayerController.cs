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
    private Vector2 move = Vector2.zero;
    private float maxSpeed = 10.0f;
    private bool isAir = false;
    private Vector3 up = Vector3.up;
    public bool inverted{
        get; private set;
    } = false;
    private Vector3 moveDir = Vector3.zero;

    // Hashs
    private int moveHash;
    private int isAirHash;
    private int jumpHash;

    // Camera
    private Vector3 cameraPos = Vector3.zero;

    // Objects
    public GameObject hand;
    private Collider hold = null;
    void Start()
    {
        // Register callbacks
        InputManager.instance.RegisterCallback(InputMap.InGame,"Vertical",OnVertical);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Horizontal",OnHorizontal);
        InputManager.instance.RegisterCallback(InputMap.InGame,"Jump",OnJump);
        InputManager.instance.RegisterCallback(InputMap.InGame, "Hold", OnHold);

        // Components
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();

        // Hashs
        moveHash = Animator.StringToHash("move");
        isAirHash = Animator.StringToHash("isAir");
        jumpHash = Animator.StringToHash("jump");
    }

    private void Update() {
        if(move.x != 0 || move.y != 0){
            
            Vector3 forward = transform.position - cameraPos;
            forward.y = 0;
            forward.Normalize();
            Vector3 right = Vector3.Cross(up, forward);
            Vector3 m = forward * move.y + right * move.x;
            m.Normalize();
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
        }
    }

    public void SetCameraPosition(Vector3 cameraPos){
        this.cameraPos = cameraPos;
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
        if(context.started){
            if(hold != null){
                hold.gameObject.transform.SetParent(null);
                hold.attachedRigidbody.isKinematic = false;
                hold = null;
            }
            else{
                var colls = Physics.OverlapSphere(hand.transform.position, 0.7f, LayerMask.GetMask("Cube"));
                if(colls.Length > 0){
                    colls[0].attachedRigidbody.isKinematic = true;
                    colls[0].gameObject.transform.SetParent(hand.transform);
                    colls[0].gameObject.transform.localPosition = Vector3.zero;
                    hold = colls[0];
                }
            }
            
        }
    }
}
