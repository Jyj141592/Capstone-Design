using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform follow;
    public CinemachineVirtualCamera cam;
    public float depth = 10.0f;
    public Vector3 offset = Vector2.zero;
    public float topRadius = 10.0f;
    public float middleRadius = 10.0f;
    public float bottomRadius = 1.0f;
    public float sensitive = 1.0f;
    private float topAngle = 80.0f;
    private float middleAngle = 0.0f;
    private float bottomAngle = -50.0f;
    private PlayerController controller;
    private float angleY = 0.0f;
    private float angleX = 0.0f;
    void Start()
    {
        controller = follow.GetComponent<PlayerController>();
        UpdatePosition();
        InputManager.instance.RegisterCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
    }
    void Update()
    {
        UpdatePosition();
    }
    private void OnDestroy() {
        if(InputManager.instance != null){
            InputManager.instance.RemoveCallback(InputMap.InGame, "ViewRotate", OnViewRotate);
        }
    }
    void UpdatePosition(){
        Vector3 pos = new Vector3(-Mathf.Sin(angleY), 0, -Mathf.Cos(angleY));
        transform.rotation = Quaternion.Euler(0, angleY, 0);
        // transform.forward = -pos;
        // transform.up = Vector3.down;

        // pos *= depth;
        // pos += transform.up * offset.y + transform.right * offset.x + follow.position;
        // transform.position = pos;
        transform.position = transform.up * offset.y + transform.right * offset.x + pos * offset.z + follow.position;
        
        float radius;
        if(angleX > middleAngle){
            radius = topRadius * ((angleX - middleAngle) / (topAngle - middleAngle)) +
                    middleRadius * ((topAngle - angleX) / (topAngle - middleAngle));
        }
        else{
            radius = middleRadius * ((angleX - bottomAngle) / (middleAngle - bottomAngle)) +
                    bottomRadius * ((middleAngle - angleX) / (middleAngle - bottomAngle));
        }   
        Vector3 p = new Vector3(0, radius * Mathf.Sin(angleX * Mathf.Deg2Rad), -radius * Mathf.Cos(angleX * Mathf.Deg2Rad));
        cam.transform.localPosition = p;
        cam.transform.localRotation = Quaternion.Euler(angleX, 0, 0);
        //cam.transform.forward = transform.position - cam.transform.position;
        controller.SetCameraPosition(cam.transform.position, cam.transform.forward);
    }
    void OnViewRotate(InputAction.CallbackContext context){
        if(context.performed){
            Vector2 value = context.ReadValue<Vector2>();
            angleY += value.x * 0.3f * sensitive;
            angleX -= value.y * 0.3f * sensitive;
            angleX = Mathf.Clamp(angleX, bottomAngle, topAngle);
        }
    }
}
