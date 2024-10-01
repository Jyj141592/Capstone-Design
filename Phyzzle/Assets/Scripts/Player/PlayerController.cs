using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector2 move;
    private float maxSpeed = 8.0f;
    public GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.AddForce(new Vector3(move.x*100, 0, move.y*100));
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
    public void OnJump(InputAction.CallbackContext callback){
        if(callback.started){
            rigid.AddForce(new Vector3(0,1000,0));
        }
    }
    public void OnHand(InputAction.CallbackContext callback){
        if(callback.started){
            Debug.Log("hand");
            Collider[] colls = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.59f), 0.5f);
            Debug.Log(colls.Length);
            if(colls.Length > 0){
                colls[0].attachedRigidbody.isKinematic = true;
                colls[0].gameObject.transform.SetParent(hand.transform);
                colls[0].gameObject.transform.localPosition = Vector3.zero;
                
            }
        }
    }
    
}
