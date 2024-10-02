using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedal : MonoBehaviour
{
    public PhysicsObject[] objects;

    private void OnCollisionEnter(Collision other) {
        for(int i = 0; i < objects.Length; i++){
            objects[i].gravityInverted = true;
        }
    }
    private void OnCollisionExit(Collision other) {
        for(int i = 0; i < objects.Length; i++){
            objects[i].gravityInverted = false;
        }
    }
}
