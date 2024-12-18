using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticityHead : ArrowHead
{
    public override string GetArrowType()
    {
        return "B";
    }

    public override void OnHit(GameObject obj)
    {
        PhysicsObject physicsObject = obj.GetComponent<PhysicsObject>();
        if(physicsObject != null){
            physicsObject.elasticity = !physicsObject.elasticity;
        }
    }
}
