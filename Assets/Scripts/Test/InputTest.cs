using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public delegate void TDD(float a);
    void Start() {
        InputManager.GetInstance().SetActive(true);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyDown", CheckInputUp);
        EventCenter.GetInstance().AddEventListener<KeyCode>("KeyUp", CheckInputDown);

        TDD rr = new TDD(InputTest.tdd);
    }

    private void CheckInputDown(KeyCode key)
    {
        Debug.Log("keydown");
    }

    private void CheckInputUp(KeyCode key)
    {
        Debug.Log("keyup");
    }

    public static void tdd(float a)
    {
        Debug.Log(a);
    }
}