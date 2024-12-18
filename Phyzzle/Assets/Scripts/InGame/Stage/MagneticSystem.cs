using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticSystem : MonoBehaviour
{
    private List<PhysicsObject> objects = new List<PhysicsObject>();
    private void FixedUpdate() {
        int cnt = objects.Count;
        if(cnt > 1){
            for(int i = 0; i < cnt - 1; i++){
                for(int j = i + 1; j < cnt; j++){
                    Vector3 dir = objects[i].transform.position - objects[j].transform.position;
                    dir.Normalize();
                    objects[j].AddMagneticForce(dir);
                    objects[i].AddMagneticForce(dir * -1);
                }
            }
        }
    }
    public void ToggleMagnetic(PhysicsObject obj){
        if(objects.Contains(obj)){
            obj.magnetic = false;
            objects.Remove(obj);
        }
        else{
            objects.Add(obj);
            obj.magnetic = true;
        }
    }
}
