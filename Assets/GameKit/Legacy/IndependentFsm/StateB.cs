using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;
using Fsm;


public class StateB : IState
{
    public void Update()
    {
        Debug.Log("This is StateB");
    }
    public void OnEnter()
    {

    }
    public void OnExit()
    {

    }
}

