using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    void Start() {
        InputManager.GetInstance().SetActive(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyDown", CheckInputUp);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyUp", CheckInputDown);
    }

    private void CheckInputDown(KeyCode key)
    {
        Debug.Log("keydown");
    }

    private void CheckInputUp(KeyCode key)
    {
        Debug.Log("keyup");
    }
}