using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private IArrowHead head;
    private float arrowSpeed = 100.0f;
    private Coroutine coroutine = null;
    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private void Start() {
        head = GetComponentInChildren<IArrowHead>();
    }
    public void Shoot(){
        coroutine = StartCoroutine(Fly());
    }

    private IEnumerator Fly(){ 
        while(true){
            transform.position = transform.position + transform.forward * arrowSpeed * Time.deltaTime;
            yield return waitForEndOfFrame;
        }
    }

    private void OnTriggerEnter(Collider other) {
        head.OnHit(other.gameObject);
        StopCoroutine(coroutine);
        coroutine = null;
        if(other.gameObject.layer == LayerMask.NameToLayer("Cube")){
            transform.SetParent(other.transform);
        }
    }
    public void SetActive(bool active){
        gameObject.SetActive(active);
    }
}
