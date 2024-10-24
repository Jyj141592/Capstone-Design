using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityButton : MonoBehaviour, IButtonAction
{
    public List<PhysicsObject> objects = new List<PhysicsObject>();

    public void OnPressed(){
        for(int i = 0; i < objects.Count; i++){
            objects[i].gravityInverted = true;
        }
    }
    public void OnReleased(){
        for(int i = 0; i < objects.Count; i++){
            objects[i].gravityInverted = false;
        }
    }
}
