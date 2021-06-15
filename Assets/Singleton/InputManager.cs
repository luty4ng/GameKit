using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : BaseManager<InputManager>
{
    private bool isActive = false;
    public InputManager()
    {
        MonoManager.GetInstance().AddUpdateListener(MyUpdate);
    }

    public void SetActive(bool boolean)
    {
        isActive = boolean;
    }
    private void CheckKeyCode(KeyCode key, String target)
    {
        if(Input.GetKeyDown(key))
        {
            EventCenter.GetInstance().EventTrigger("KeyDown", key);
        }
        if(Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("KeyUp", key);
        }
    }

    private void CheckKeyCode(KeyCode key)
    {
        if(Input.GetKeyDown(key))
        {
            EventCenter.GetInstance().EventTrigger("KeyDown", key);
        }
        if(Input.GetKeyUp(key))
        {
            EventCenter.GetInstance().EventTrigger("KeyUp", key);
        }
    }

    private void CheckButton(String name, String target)
    {
        if(Input.GetButtonDown(name))
            EventCenter.GetInstance().EventTrigger(target + "ButtonDown", name);
        
        if(Input.GetButtonUp(name))
            EventCenter.GetInstance().EventTrigger(target + "ButtonUp", name);
    }

    private void CheckButton(String name)
    {
        if(Input.GetButtonDown(name))
            EventCenter.GetInstance().EventTrigger("ButtonDown", name);
        
        if(Input.GetButtonUp(name))
            EventCenter.GetInstance().EventTrigger("ButtonUp", name);
    }

    private void CheckAxis()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        (float, float) Axis = (Horizontal, Vertical);
        EventCenter.GetInstance().EventTrigger<(float, float)>("Axis", Axis);
    }

    private void CheckAxis(String target)
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        (float, float) Axis = (Horizontal, Vertical);
        EventCenter.GetInstance().EventTrigger<(float, float)>(target + "Axis", Axis);
    }

    private void CheckAxisRaw()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        (float, float) AxisRaw = (Horizontal, Vertical);
        EventCenter.GetInstance().EventTrigger<(float, float)>("AxisRaw", AxisRaw);
    }

    private void CheckAxisRaw(String target)
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        (float, float) AxisRaw = (Horizontal, Vertical);
        EventCenter.GetInstance().EventTrigger<(float, float)>(target + "AxisRaw", AxisRaw);
    }

    private void MyUpdate()
    {
        if(!isActive)
            return;

        CheckAxis();
        CheckAxisRaw();
    }
}
