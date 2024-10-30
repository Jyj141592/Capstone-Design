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
    public bool magnetic{
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
    private float impulse = 200.0f;
    private float magneticValue = 700.0f;
    private Vector3 magneticForce = Vector3.zero;

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
            if(magnetic){
                rigid.AddForce(magneticForce);
                magneticForce = Vector3.zero;
            }
        }
    }
    public void Explode(){
        if(isKinematic) return;
        var colls = Physics.OverlapSphere(transform.position, 5);
        for(int j = 0; j < colls.Length; j++){
            PhysicsObject p = colls[j].GetComponent<PhysicsObject>();
            if(p != null){
                Vector3 dir = colls[j].transform.position - transform.position;
                dir.Normalize();
                p.AddForce(dir * impulse, ForceMode.Impulse);
            }
        }
    }

    private void AddGravity(){
        float acc = gravityInverted ? -gravity : gravity;
        rigid.AddForce(Vector3.up * rigid.mass * acc);
    }
    public void Move(Vector3 position){
        rigid.MovePosition(position);
    }
    public void AddMagneticForce(Vector3 direction){
        magneticForce += direction * magneticValue;
    }
    public void SetVelocity(Vector3 velocity){
        rigid.velocity = velocity;
    }
    public void SetMass(float mass){
        rigid.mass = mass;
    }
}
