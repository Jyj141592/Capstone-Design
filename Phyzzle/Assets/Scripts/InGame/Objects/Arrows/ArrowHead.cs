using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArrowHead : MonoBehaviour
{
    public abstract void OnHit(GameObject obj);
    public abstract string GetArrowType();
}
