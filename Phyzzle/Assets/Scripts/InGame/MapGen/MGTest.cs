using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MGTest : MonoBehaviour
{
    public GameObject btn;
    public TMP_Text btnText;
    public Image pzImg1;
    public Image pzImg2;
    public Image pzImg3;
    public Image pzImg4;
    public Image pzImg5;
    public TMP_Text pzTxt1;
    public TMP_Text pzTxt2;
    public TMP_Text pzTxt3;
    public TMP_Text pzTxt4;
    public TMP_Text pzTxt5;
    private int step;

    // Start is called before the first frame update
    void Start()
    {
        step = 0;
        pzImg1.enabled = false;
        pzImg2.enabled = false;
        pzImg3.enabled = false;
        pzImg4.enabled = false;
        pzImg5.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowNext()
    {
        switch(step)
        {
            case 0: ShowNext1(); break;
            case 1: ShowNext2(); break;
            case 2: ShowNext3(); break;
        }
    }

    private void ShowNext1()
    {
        step = 1;
        pzImg1.enabled = true;
        pzImg2.enabled = true;
        pzImg3.enabled = true;
        pzImg4.enabled = true;
        pzImg5.enabled = true;
        pzTxt1.SetText("??");
        pzTxt2.SetText("??\n??");
        pzTxt3.SetText("??");
        pzTxt4.SetText("??");
        pzTxt5.SetText("??\n??");
        btnText.SetText("랜덤 채우기");
    }

    private void ShowNext2()
    {
        step = 2;
        pzTxt1.SetText("중력 반전");
        pzTxt2.SetText("탄성력\n폭발");
        pzTxt3.SetText("자력");
        pzTxt4.SetText("중력 반전");
        pzTxt5.SetText("중력 반전\n자력");
        btnText.SetText("의존성 추가");
    }

    private void ShowNext3()
    {
        step = 3;
        pzTxt1.SetText("중력 반전");
        pzTxt2.SetText("탄성력\n폭발");
        pzTxt3.SetText("자력");
        pzTxt4.SetText("<#ff0000>중력 반전</color>");
        pzTxt5.SetText("<#ff0000>중력 반전</color>\n자력");
        btnText.SetText("개발중");
    }
}
