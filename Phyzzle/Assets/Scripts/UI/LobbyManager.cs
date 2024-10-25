using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject customPanel;
    public GameObject prefPanel;
    public GameObject popupPanel;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.SetActive(true);
        customPanel.SetActive(false);
        prefPanel.SetActive(false);
        popupPanel.SetActive(true); // if 이전 게임 exist
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchToMain()
    {
        mainPanel.SetActive(true);
        customPanel.SetActive(false);
    }

    public void SwitchToCustom()
    {
        mainPanel.SetActive(false);
        customPanel.SetActive(true);
    }

    public void ShowPref()
    {
        // TODO 기존값 불러오기
        prefPanel.SetActive(true);
    }

    public void HidePref()
    {
        // TODO 설정값 저장하기
        prefPanel.SetActive(false);
    }

    public void LoadOldGame()
    {
        // 이전 게임 불러오기
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
