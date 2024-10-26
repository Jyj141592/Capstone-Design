using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticButton : MonoBehaviour, IButtonAction
{
    public List<PhysicsObject> objects = new List<PhysicsObject>();
    public MagneticSystem magneticSystem;
    public void OnPressed(){
        for(int i = 0; i < objects.Count; i++){
            magneticSystem.ToggleMagnetic(objects[i]);
        }
    }
    public void OnReleased(){
        for(int i = 0; i < objects.Count; i++){
            magneticSystem.ToggleMagnetic(objects[i]);
        }
    }
}
