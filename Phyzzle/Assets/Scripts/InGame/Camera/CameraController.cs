using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private float topAngle = 89.0f;
    private float bottomAngle = -89.0f;
    private float angleX = 0.0f;
    // private Coroutine walkingCoroutine = null;
    // private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    public void UpdateRotation(float angle){
        angleX += angle;
        angleX = Mathf.Clamp(angleX, bottomAngle, topAngle);
        transform.localRotation = Quaternion.Euler(angleX, 0, 0);
    }

    public void Walk(){
        // if(walkingCoroutine == null){
        //     walkingCoroutine = StartCoroutine(Walking());

        // }
    }
    public void StopWalk(){
        // if(walkingCoroutine != null){
        //     StopCoroutine(walkingCoroutine);
        //     walkingCoroutine = null;
        // }
    }
    // private IEnumerator Walking()
    // {
    //     while (true)
    //     {
    //         yield return waitForEndOfFrame;
    //     }
    // }
}
