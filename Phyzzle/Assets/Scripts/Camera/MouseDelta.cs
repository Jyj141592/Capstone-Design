using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class MouseDelta : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    // Start is called before the first frame update
    public void OnMouseDelta(InputAction.CallbackContext callback){
        if(callback.started){
            Vector2 mouseDelta = callback.ReadValue<Vector2>();
            freeLookCamera.m_XAxis.Value += mouseDelta.x * Time.deltaTime * 50f; // X축 회전 속도 조절
            freeLookCamera.m_YAxis.Value -= mouseDelta.y * Time.deltaTime * 0.6f;   // Y축 회전 속도 조절
        }
    }
}
