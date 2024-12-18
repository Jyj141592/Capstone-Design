using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHead : ArrowHead
{

    public override void OnHit(GameObject obj)
    {
        PhysicsObject physicsObject = obj.GetComponent<PhysicsObject>();
        if(physicsObject != null){
            physicsObject.Explode();
        }
    }
    public override string GetArrowType()
    {
        return "E";
    }
}
