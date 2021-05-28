using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using FSM.NonSO;

namespace FSM.NonSO
{
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
}
