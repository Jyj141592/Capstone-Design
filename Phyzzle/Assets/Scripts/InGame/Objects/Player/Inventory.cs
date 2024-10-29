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
    public int selected{
        get; set;
    } = -1;
    public OnInventoryUpdated onInventoryUpdated;

    public ArrowController arrow;
    private void Start() {
        AddArrow(arrow);
        selected = 0;
    }

    public void AddArrow(ArrowController arrow){
        arrows.Add(arrow);
        if(onInventoryUpdated != null){
            onInventoryUpdated();
        }
    }
    public void UseArrow(){
        arrows.RemoveAt(selected);
        if(arrows.Count <= selected) selected = arrows.Count - 1;
        if(onInventoryUpdated != null){
            onInventoryUpdated();
        }
    }
    public ArrowController GetSelectedArrow(){
        if(selected == -1){
            return null;
        }
        else{
            return arrows[selected];
        }
    }
}
