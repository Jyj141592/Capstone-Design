using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHead : ArrowHead
{
    public override void OnHit(GameObject obj)
    {
        PhysicsObject physicsObject = obj.GetComponent<PhysicsObject>();
        if(physicsObject != null){
            physicsObject.gravityInverted = !physicsObject.gravityInverted;
        }
    }
    public override string GetArrowType(){
        return "G";
    }
}
