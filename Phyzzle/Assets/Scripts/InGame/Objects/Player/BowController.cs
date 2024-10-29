using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    public delegate void OnAim(bool aim);
    public OnAim onAim;
    private Inventory inventory;
    private ArrowController arrow = null;
    public void SetInventory(Inventory inventory){
        this.inventory = inventory;
    }
    public void StartTarget(){
        gameObject.SetActive(true);
        arrow = inventory.GetSelectedArrow();
        if(arrow != null){
            arrow.SetActive(true);
            //arrow.transform.position = transform.position;
            arrow.transform.SetParent(transform);
            arrow.transform.localPosition = Vector3.zero;
            arrow.transform.localRotation = Quaternion.identity;
        }
        if(onAim != null){
            onAim(true);
        }
    }
    public void StopTarget(){
        gameObject.SetActive(false);
        if(arrow != null){
            arrow.transform.SetParent(null);
            arrow.SetActive(false);
            arrow = null;
        }
        if(onAim != null){
            onAim(false);
        }
    }
    public void Shoot(){
        if(arrow != null){
            arrow.transform.SetParent(null);
            arrow.Shoot();
            arrow = null;
            inventory.UseArrow();
            StartTarget();
        }
    }
}
