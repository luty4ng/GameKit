using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingPanel : BasePanel
{
    public void ChangerBGMSlider(Slider slider)
    {
        float value = slider.value;
        AudioManager.GetInstance().ChangeBGMVolume(value / 100);
        Debug.Log(value);
    }

    public void ChangerMasterSlider(Slider slider)
    {
        float value = slider.value;
        AudioManager.GetInstance().ChangeBGMVolume(value / 100);
        AudioManager.GetInstance().ChangeSoundVolume(value / 100);
        Debug.Log(value);
    }

    public void ChangerSoundSlider(Slider slider)
    {
        float value = slider.value;
        AudioManager.GetInstance().ChangeSoundVolume(value  / 100);
        Debug.Log(value);
    }

    public void SettingToPause()
    {
        UIManager.GetInstance().HidePanel("SettingPanel");
        UIManager.GetInstance().ShowPanel<PausePanel>("PausePanel");
    }
}
