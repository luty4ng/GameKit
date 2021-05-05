using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TinyUI_Manager : MonoBehaviour
{

    public GameObject pauseProp;
    public GameObject settingProp;
    public Text mastserSliderText;
    public Text bgmSliderText;
    public Text soundSliderText;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pauseProp.activeInHierarchy)
        {
            Time.timeScale = 0;
            InputManager.GetInstance().SetActive(false);
            pauseProp.SetActive(true);
		    Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && pauseProp.activeInHierarchy)
        {
            Time.timeScale = 1;
            pauseProp.SetActive(false);
            InputManager.GetInstance().SetActive(true);
		    Cursor.visible = false;
            //Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public  void PauseToSetting()
    {
        pauseProp.SetActive(false);
        settingProp.SetActive(true);
    }

    public void SettingToPause()
    {
        pauseProp.SetActive(true);
        settingProp.SetActive(false);
    }

    public void ChangerBGMSlider(Slider slider)
    {
        float value = slider.value;
        bgmSliderText.text = value.ToString();
        AudioManager.GetInstance().ChangeBGMVolume(value / 100);
        Debug.Log(value);
    }

    public void ChangerMasterSlider(Slider slider)
    {
        float value = slider.value;
        mastserSliderText.text = value.ToString();
        AudioManager.GetInstance().ChangeBGMVolume(value / 100);
        AudioManager.GetInstance().ChangeSoundVolume(value / 100);
        Debug.Log(value);
    }

    public void ChangerSoundSlider(Slider slider)
    {
        float value = slider.value;
        soundSliderText.text = value.ToString();
        AudioManager.GetInstance().ChangeSoundVolume(value  / 100);
        Debug.Log(value);
    }
}
