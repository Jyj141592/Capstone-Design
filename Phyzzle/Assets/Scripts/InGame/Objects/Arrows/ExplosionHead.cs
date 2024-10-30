using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHead : ArrowHead
{

    public override void OnHit(GameObject obj)
    {
        
    }
    public override string GetArrowType()
    {
        return "E";
    }
}
