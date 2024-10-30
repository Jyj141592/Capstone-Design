using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public ArrowHead head;
    private float arrowSpeed = 100.0f;
    private bool isFlying = false;
    private Coroutine coroutine = null;
    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private int playerLayer;

    private void Start() {
        //head = GetComponentInChildren<ArrowHead>();
        playerLayer = LayerMask.NameToLayer("Player");
    }
    public void Shoot(){
        isFlying = true;
        coroutine = StartCoroutine(Fly());
    }

    private IEnumerator Fly(){ 
        while(true){
            transform.position = transform.position + transform.forward * arrowSpeed * Time.deltaTime;
            yield return waitForEndOfFrame;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(isFlying){
            if(other.gameObject.layer == playerLayer) return;
            head.OnHit(other.gameObject);
            StopCoroutine(coroutine);
            coroutine = null;
            if(other.gameObject.layer == LayerMask.NameToLayer("Cube")){
                transform.SetParent(other.transform);
            }
            isFlying = false;
        }
    }
    public string GetArrowType(){
        return head.GetArrowType();
    }
    public void SetActive(bool active){
        gameObject.SetActive(active);
    }
}
