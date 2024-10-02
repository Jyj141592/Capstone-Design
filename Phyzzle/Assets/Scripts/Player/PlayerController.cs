using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    private PhysicsObject po;
    private Vector2 move;
    private float maxSpeed = 12.0f;
    private Vector3 fo = new Vector3(0,0,1);
    public GameObject hand;
    private Collider hold = null;
    public CinemachineFreeLook cam;
    // public CinemachineBrain brain;
    // private bool inverted = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        po = GetComponent<PhysicsObject>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 forward = transform.position - cam.transform.position;
        forward.y = 0;
        forward.Normalize();
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        // Vector3 forward = cameraTransform.forward;
        // Vector3 right = cameraTransform.right;
        // Debug.Log(forward);
        // forward.y = 0.0f; right.y = 0.0f;
        // forward.Normalize(); right.Normalize();
        Vector3 m = forward*move.y + right*move.x;
        if(move.x != 0.0f || move.y != 0.0f){
            transform.forward = m;
            fo = forward.normalized;
        }
        rigid.AddForce(m*100);
        float v = (rigid.velocity.x*rigid.velocity.x) + (rigid.velocity.z*rigid.velocity.z);
        if(v > maxSpeed*maxSpeed){
            rigid.velocity = new Vector3((rigid.velocity.x * maxSpeed)/v, rigid.velocity.y, (rigid.velocity.z*maxSpeed)/v);
        }
    }

    public void OnMove(InputAction.CallbackContext callback){
        if(callback.started){
            move = callback.ReadValue<Vector2>();
        }
        else if(callback.canceled){
            move = Vector2.zero;
        }
    }
    public void OnVertical(InputAction.CallbackContext callback){
        if(callback.started){
            move.y = callback.ReadValue<float>();
        }
        else if(callback.canceled){
            move.y = 0;
        }
    }
    public void OnHorizontal(InputAction.CallbackContext callback){
        if(callback.started){
            move.x = callback.ReadValue<float>();
        }
        else if(callback.canceled){
            move.x = 0;
        }
    }
    public void OnJump(InputAction.CallbackContext callback){
        if(callback.started){
            float force = 1000;
            if(po.gravityInverted) force = -1000;
            rigid.AddForce(new Vector3(0,force,0));
        }
    }
    public void OnHand(InputAction.CallbackContext callback){
        if(callback.started){
            if(hold == null){
                Collider[] colls = Physics.OverlapSphere(transform.position + (fo * 1.59f), 0.7f, LayerMask.GetMask("Cube"));
                if(colls.Length > 0){
                    colls[0].attachedRigidbody.isKinematic = true;
                    colls[0].gameObject.transform.SetParent(hand.transform);
                    colls[0].gameObject.transform.localPosition = Vector3.zero;
                    hold = colls[0];
                }
            }
            else{
                hold.gameObject.transform.SetParent(null);
                hold.attachedRigidbody.isKinematic = false;
                hold = null;
            }
        }
    }
    // private void OnCollisionEnter(Collision other) {
    //     if(other.gameObject.layer == LayerMask.NameToLayer("Ground")){
    //         if(inverted != po.gravityInverted){
    //             inverted = po.gravityInverted;
    //             if(inverted){
    //                 transform.Rotate(new Vector3(0,0,180));
    //                 brain.m_WorldUpOverride
    //             }
    //             else{

    //             }
    //         }
    //     }
    // }
}
