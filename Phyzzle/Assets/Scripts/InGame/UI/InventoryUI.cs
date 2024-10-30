using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private Inventory inventory = null;
    public List<ItemUI> items;
    private int selected = -1;

    public void SetInventory(Inventory inventory){
        this.inventory = inventory;
        inventory.onInventoryUpdated += OnInventoryUpdated;
    }
    public void OnInventoryUpdated(){
        for(int i = 0; i < 4; i++){
            if(i < inventory.arrows.Count){
                items[i].SetText(inventory.arrows[i].GetArrowType());
            }
            else{
                items[i].SetText("");
            }
        }
        ChangeSelect(inventory.selected);
    }
    public void OnSelect(int idx){
        if(idx < inventory.arrows.Count){
            inventory.selected = idx;
            ChangeSelect(idx);
        }
    }
    private void ChangeSelect(int idx){
        if(idx == selected) return;
        if(selected != -1){
            items[selected].SetSelected(false);
        }
        if(idx != -1){
            items[idx].SetSelected(true);
        }
        selected = idx;
    }
}
