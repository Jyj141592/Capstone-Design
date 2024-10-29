using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHead : MonoBehaviour, IArrowHead
{
    public void OnHit(GameObject obj)
    {
        PhysicsObject physicsObject = obj.GetComponent<PhysicsObject>();
        if(physicsObject != null){
            physicsObject.gravityInverted = !physicsObject.gravityInverted;
        }
    }
}
