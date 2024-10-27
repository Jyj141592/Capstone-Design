using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsObject : MonoBehaviour
{
    // Component
    protected Rigidbody rigid;
    
    // Status
    public bool gravityInverted{
        get; set;
    } = false;
    public bool isKinematic{
        get; set;
    } = false;

    public bool freezeRotation{
        get => rigid.freezeRotation;
        set => rigid.freezeRotation = value;
    }

    private float gravity = -40.0f;

    public virtual void Start() {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
    }
    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force){
        if(!isKinematic){
            rigid.AddForce(force, forceMode);
        }
    }

    private void FixedUpdate() {
        if(!isKinematic){
            AddGravity();
        }
    }

    private void AddGravity(){
        float acc = gravityInverted ? -gravity : gravity;
        rigid.AddForce(Vector3.up * rigid.mass * acc);
    }
    public void Move(Vector3 position){
        rigid.MovePosition(position);
    }
    public void SetVelocity(Vector3 velocity){
        rigid.velocity = velocity;
    }
    public void SetMass(float mass){
        rigid.mass = mass;
    }
}
