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

    private void CheckMovement()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        EventCenter.GetInstance().EventTrigger<float>("Horizontal", Horizontal);
        EventCenter.GetInstance().EventTrigger<float>("Vertical", Vertical);
    }

    private void CheckButtonDown(String name)
    {
        if(Input.GetButtonDown(name))
            EventCenter.GetInstance().EventTrigger(name + " ButtonDown");
    }

    private void CheckButtonUp(string name)
    {
        if(Input.GetButtonUp(name))
            EventCenter.GetInstance().EventTrigger(name + " ButtonUp");
    }

    private void MyUpdate()
    {
        if(!isActive)
            return;
        CheckKeyCode(KeyCode.Q);
        CheckKeyCode(KeyCode.E);
        //CheckKeyCode(KeyCode.S);
        //CheckKeyCode(KeyCode.D);

        CheckMovement();
        CheckButtonDown("Jump");
        CheckButtonDown("Fire1");
        CheckButtonUp("Fire1");
    }
}
