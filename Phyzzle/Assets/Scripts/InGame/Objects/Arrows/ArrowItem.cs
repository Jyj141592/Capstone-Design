using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowItem : MonoBehaviour
{
    public ArrowController arrow;
    private int playerLayer;
    private void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
    }
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == playerLayer){
            if(other.gameObject.GetComponent<Inventory>().AddArrow(arrow)){
                gameObject.SetActive(false);
            }
        }
    }
}
