using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.NonSO;

namespace FSM.NonSO
{
    public interface IState
    {
        void Update();
        void OnEnter();
        void OnExit();
    }
}


