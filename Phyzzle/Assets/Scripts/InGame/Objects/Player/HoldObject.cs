using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : MonoBehaviour
{
    public bool isHold{
        get; private set;
    } = false;
    private Collider hold = null;
    private PhysicsObject holdPhysics = null;
    private LayerMask cubeMask;
    private Coroutine attachObject = null;
    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Start() {
        cubeMask = LayerMask.GetMask("Cube");
    }
    public void Hold(){
        if(isHold){
            ReleaseHold();
        }
        else{
            var colls = Physics.OverlapSphere(transform.position, 0.7f, cubeMask);
            if(colls.Length > 0){
                isHold = true;
                hold = colls[0];
                holdPhysics = hold.GetComponent<PhysicsObject>();
                holdPhysics.freezeRotation = true;
                holdPhysics.isKinematic = true;
                //hold.transform.position = transform.position;

                if(attachObject != null){
                    StopCoroutine(attachObject);
                    attachObject = null;
                }
                attachObject = StartCoroutine(AttachObject());
            }
        }
    }
    public void Throw(){
        if(isHold){
            PhysicsObject obj = holdPhysics;
            ReleaseHold();
            obj.AddForce(transform.forward * 100, ForceMode.Impulse);
        }
    }
    private void ReleaseHold(){
        holdPhysics.isKinematic = false;
        holdPhysics.freezeRotation = false;
        holdPhysics = null;
        hold = null;
        isHold = false;
        if(attachObject != null){
            StopCoroutine(attachObject);
            attachObject = null;
        }
    }

    private IEnumerator AttachObject(){
        while(isHold){
            Vector3 dir = transform.position - hold.transform.position;
            float back = 1.0f;
            if(Vector3.Dot(transform.forward, dir) > 0) back = 8.0f;
            float dist = dir.sqrMagnitude;
            holdPhysics.SetVelocity(dir * (dist + 1) * back);
            yield return waitForFixedUpdate;
        }
    }
    private void OnDestroy() {
        if(attachObject != null){
            StopCoroutine(attachObject);
            attachObject = null;
        }
    }
}
