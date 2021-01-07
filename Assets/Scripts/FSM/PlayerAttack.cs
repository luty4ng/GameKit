using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : States {
    public PlayerAttack()
    {
        stateID = StateID.PlayerAttack;
        AddTransition(Transition.PlayerFocus, StateID.PlayerFocus);
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    public override void Excute<StateController>(StateController player)
    {
        Debug.Log("Player Attack");
        Decision<StateController>(player);
    }

    public override void Decision<StateController>(StateController player)
    {
        // 未来可以封装Unity生命周期的监听方法（MonoManager）
        
        Debug.Log("Reasons Attack");
        if(Input.GetKeyDown(KeyCode.W))
        {
            StateManager.GetInstance().PerformTransition(Transition.PlayerFocus);
        }
        
    }



    
}