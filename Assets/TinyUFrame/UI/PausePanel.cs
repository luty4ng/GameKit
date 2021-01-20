using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PausePanel : BasePanel
{
    // Start is called before the first frame update
    public void Quit()
    {
        Application.Quit();
    }

    public void PauseToSetting()
    {
        UIManager.GetInstance().HidePanel("PausePanel");
        UIManager.GetInstance().ShowPanel<SettingPanel>("SettingPanel");
    }
}
