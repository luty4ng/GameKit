using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit
{
    public class InputManager : SingletonBase<InputManager>
{
    private bool isActive = false;
    private InputManager inputManager;

    public float horizontal
    {
        get
        {
            if (isActive)
                return Input.GetAxisRaw("Horizontal");
            else
                return 0;
        }
    }

    public float vertical
    {
        get
        {
            if(isActive)
                return Input.GetAxisRaw("Vertical");
            else
                return 0;
        }
    }
    public void SetActive(bool boolean) => isActive = boolean;

        public bool GetKey(KeyCode key)
        {
            if (!isActive)
                return false;
            return Input.GetKey(key);
        }

        public bool GetKeyDown(KeyCode key)
        {
            if (!isActive)
                return false;
            return Input.GetKeyDown(key);
        }

        public bool GetKeyUp(KeyCode key)
        {
            if (!isActive)
                return false;
            return Input.GetKeyUp(key);
        }
    
    public InputManager()
    {
        MonoManager.instance.AddUpdateListener(MyUpdate);
    }
    
    private void CheckKeyCode(KeyCode key, String target)
    {
        if (Input.GetKeyDown(key))
        {
            EventManager.instance.EventTrigger("KeyDown", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventManager.instance.EventTrigger("KeyUp", key);
        }
    }

    private void CheckKeyCode(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            EventManager.instance.EventTrigger("KeyDown", key);
        }
        if (Input.GetKeyUp(key))
        {
            EventManager.instance.EventTrigger("KeyUp", key);
        }
    }

    private void CheckButton(String name, String target)
    {
        if (Input.GetButtonDown(name))
            EventManager.instance.EventTrigger(target + "ButtonDown", name);

        if (Input.GetButtonUp(name))
            EventManager.instance.EventTrigger(target + "ButtonUp", name);
    }

    private void CheckButton(String name)
    {
        if (Input.GetButtonDown(name))
            EventManager.instance.EventTrigger("ButtonDown", name);

        if (Input.GetButtonUp(name))
            EventManager.instance.EventTrigger("ButtonUp", name);
    }

    private void CheckAxis()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        (float, float) Axis = (Horizontal, Vertical);
        EventManager.instance.EventTrigger<(float, float)>("Axis", Axis);
    }

    private void CheckAxis(String target)
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        (float, float) Axis = (Horizontal, Vertical);
        EventManager.instance.EventTrigger<(float, float)>(target + "Axis", Axis);
    }

    private void CheckAxisRaw()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        (float, float) AxisRaw = (Horizontal, Vertical);
        EventManager.instance.EventTrigger<(float, float)>("AxisRaw", AxisRaw);
    }

    private void CheckAxisRaw(String target)
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        (float, float) AxisRaw = (Horizontal, Vertical);
        EventManager.instance.EventTrigger<(float, float)>(target + "AxisRaw", AxisRaw);
    }

    private void MyUpdate()
    {
        if (!isActive)
            return;

        CheckAxis();
        CheckAxisRaw();
    }
}

}
