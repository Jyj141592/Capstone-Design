using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : PhysicsObject
{
    // move
    private Vector3 moveDir = Vector3.zero;
    public float maxSpeed = 10.0f;

    public override void Start()
    {
        base.Start();
    }
    public void SetMoveDirection(Vector3 direction){
        if(direction != moveDir){
            float l = Vector3.Dot(rigid.velocity, moveDir);
            if(l < maxSpeed){
                rigid.velocity = rigid.velocity - moveDir * l;
            }
            else{
                rigid.velocity = rigid.velocity - moveDir * maxSpeed;
            }
        }
        float len = Vector3.Dot(rigid.velocity, direction);
        if(len < maxSpeed){
            rigid.velocity += (maxSpeed - len) * direction;
        }
        moveDir = direction;
    }
}
