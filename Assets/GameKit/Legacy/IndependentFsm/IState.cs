using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fsm
{
    public interface IState
    {
        void Update();
        void OnEnter();
        void OnExit();
    }
}


