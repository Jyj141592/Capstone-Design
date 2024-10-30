using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnInventoryUpdated();
    public List<ArrowController> arrows{
        get; private set;
    } = new List<ArrowController>();
    private int _selected = -1;
    public int selected{
        get => _selected;
        set {
            if(arrows.Count > value){
                _selected = value;
            }
        }
    }
    public OnInventoryUpdated onInventoryUpdated;

    // public ArrowController arrow;
    // private void Start() {
    //     selected = 0;
    //     AddArrow(arrow);
    // }

    public bool AddArrow(ArrowController arrow){
        if(arrows.Count < 4){
            arrows.Add(arrow);
            arrow.transform.SetParent(transform);
            arrow.transform.localPosition = Vector3.zero;
            arrow.gameObject.SetActive(false);
            if(arrows.Count == 1) selected = 0;
            if(onInventoryUpdated != null){
                onInventoryUpdated();
            }
            return true;
        }
        else return false;
    }
    public void UseArrow(){
        arrows.RemoveAt(selected);
        if(arrows.Count <= selected) {
            selected = arrows.Count - 1;
        }
        if(onInventoryUpdated != null){
            onInventoryUpdated();
        }
    }
    public ArrowController GetSelectedArrow(){
        if(selected < 0){
            return null;
        }
        else{
            return arrows[selected];
        }
    }
}
