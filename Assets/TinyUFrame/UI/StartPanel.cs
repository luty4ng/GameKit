using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartPanel : BasePanel
{
    void Start()
    {
        //Button play = GetComponentInDic<Button>("Play");
    }
    public void Play()
    {
        Debug.Log("Play Button Pushed");
        UIManager.GetInstance().HidePanel("StartPanel");
    }

    public void Quit()
    {
        Application.Quit();
    }
}