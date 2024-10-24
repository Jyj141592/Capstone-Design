using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsObject : MonoBehaviour
{
    // Component
    protected Rigidbody rigid;
    
    // Status
    private bool _gravityInverted = false;
    public bool gravityInverted{
        get => _gravityInverted;
        set {
            if(value != _gravityInverted){
                if(value){
                    rigid.useGravity = false;
                }
                else{
                    rigid.useGravity = true;
                }
                _gravityInverted = value;
            }
        }
    }
    public virtual void Start() {
        rigid = GetComponent<Rigidbody>();
    }
    public void AddForce(Vector3 force){
        rigid.AddForce(force);
    }

    private void FixedUpdate() {
        if(gravityInverted){
            AddGravity();
        }
    }

    private void AddGravity(){
        rigid.AddForce(Vector3.up * rigid.mass * 40);
    }
}
