using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticHead : ArrowHead
{
    public MagneticSystem magneticSystem = null;
    public override void OnHit(GameObject obj)
    {
        if(magneticSystem == null) return;
        PhysicsObject physicsObject = obj.GetComponent<PhysicsObject>();
        if(physicsObject != null){
            magneticSystem.ToggleMagnetic(physicsObject);
        }
    }
    public override string GetArrowType()
    {
        return "M";
    }
}
