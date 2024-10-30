using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsObject : MonoBehaviour
{
    // Component
    protected Rigidbody rigid;
    private Collider coll;
    
    // Status
    public bool gravityInverted{
        get; set;
    } = false;
    public bool magnetic{
        get; set;
    } = false;
    private bool _elasticity = false;
    public bool elasticity{
        get => _elasticity; 
        set {
            if(value){
                coll.material = elasticMaterial;
            }
            else{
                coll.material = idle;
            }
            _elasticity = value;
        }
    }
    public bool isKinematic{
        get; set;
    } = false;

    public bool freezeRotation{
        get => rigid.freezeRotation;
        set => rigid.freezeRotation = value;
    }

    private float gravity = -40.0f;
    private float impulse = 200.0f;
    private float magneticValue = 1000.0f;
    private Vector3 magneticForce = Vector3.zero;
    public PhysicMaterial idle;
    public PhysicMaterial elasticMaterial;

    public virtual void Start() {
        rigid = GetComponent<Rigidbody>();
        rigid.useGravity = false;
        coll = GetComponent<Collider>();
    }
    public void AddForce(Vector3 force, ForceMode forceMode = ForceMode.Force){
        if(!isKinematic){
            rigid.AddForce(force, forceMode);
        }
    }

    private void FixedUpdate() {
        if(!isKinematic){
            AddGravity();
            // if(magnetic){
            //     rigid.AddForce(magneticForce);
            //     magneticForce = Vector3.zero;
            // }
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
        if(!isKinematic){
            //magneticForce += direction * magneticValue;
            rigid.AddForce(direction * magneticValue);
        }
    }
    public void SetVelocity(Vector3 velocity){
        rigid.velocity = velocity;
    }
    public void SetMass(float mass){
        rigid.mass = mass;
    }
}
