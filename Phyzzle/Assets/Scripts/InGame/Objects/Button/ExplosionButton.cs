using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionButton : MonoBehaviour, IButtonAction
{
    public List<PhysicsObject> objects = new List<PhysicsObject>();
    // private float impulse = 200.0f;
    // private LayerMask mask;
    // private void Start() {
    //     mask = ~LayerMask.GetMask("Ground");
    // }
    public void OnPressed()
    {
        for(int i = 0; i < objects.Count; i++){
            objects[i].Explode();
        }
    }

    public void OnReleased()
    {
        
    }
}
