using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.SO;

public class AttackState : States {
    public AttackState()
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

    public override void Excute<PlayerController>(PlayerController player)
    {
        Debug.Log("Player Attack");
        Decision<PlayerController>(player);
    }

    public override void Decision<PlayerController>(PlayerController player)
    {

        Debug.Log("Reasons Attack");
    }



    
}