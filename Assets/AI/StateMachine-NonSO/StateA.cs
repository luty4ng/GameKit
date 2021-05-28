using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FSM.NonSO;

namespace FSM.NonSO
{
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
}