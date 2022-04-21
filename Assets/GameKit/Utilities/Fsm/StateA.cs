using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;
using Fsm;


public class StateA : IState
{
    public void Update()
    {
        Debug.Log("This is StateA");
    }
    public void OnEnter()
    {

    }
    public void OnExit()
    {

    }
}
