using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Image image;
    private void Start() {
        text = GetComponentInChildren<TextMeshProUGUI>();
        image = GetComponent<Image>();
    }
    public void SetText(string txt){
        text.text = txt;
    }
    public void SetSelected(bool active){
        if(active){
            image.color = Color.yellow;
        }
        else{
            image.color = Color.white;
        }
    }
}
