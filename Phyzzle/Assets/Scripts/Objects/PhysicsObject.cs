using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    private bool _gravityInverted = false;
    public bool gravityInverted{
        get => _gravityInverted;
        set {
            if(_gravityInverted != value){
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
    private Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        if(gravityInverted){
            rigid.AddForce(new Vector3(0,30*rigid.mass,0));
        }
    }
}
