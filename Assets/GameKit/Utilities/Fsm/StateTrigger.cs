using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;
using Fsm;

public class StateTrigger : MonoBehaviour
{
    private StateMachine stateMachine;
    [SerializeField] private string currentState;
    private void Awake()
    {
        stateMachine = new StateMachine();
        StateA stateA = new StateA();
        StateB stateB = new StateB();
        System.Func<bool> AToB() => () => Random.Range(0f, 1f) >= 0.3f;
        System.Func<bool> BToA() => () => Random.Range(0f, 1f) >= 0.6f;
        stateMachine.AddTransition(stateA, stateB, AToB());
        stateMachine.AddTransition(stateB, stateA, BToA());

        stateMachine.SetState(stateA);
    }

    void Update()
    {
        stateMachine.Update();
        currentState = stateMachine.GetCurrentState().ToString();
    }
}
