using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionButton : MonoBehaviour, IButtonAction
{
    public List<PhysicsObject> objects = new List<PhysicsObject>();
    private float impulse = 200.0f;
    private LayerMask mask;
    private void Start() {
        mask = ~LayerMask.GetMask("Ground");
    }
    public void OnPressed()
    {
        for(int i = 0; i < objects.Count; i++){
            var colls = Physics.OverlapSphere(objects[i].transform.position, 5, mask);
            for(int j = 0; j < colls.Length; j++){
                PhysicsObject p = colls[j].GetComponent<PhysicsObject>();
                if(p != null){
                    Vector3 dir = colls[j].transform.position - objects[i].transform.position;
                    dir.Normalize();
                    p.AddForce(dir * impulse, ForceMode.Impulse);
                }
            }
        }
    }

    public void OnReleased()
    {
        
    }
}
