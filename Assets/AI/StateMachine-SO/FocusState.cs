using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM.SO;

public class FocusState : States {
    public FocusState()
    {
        stateID = StateID.PlayerFocus;
        AddTransition(Transition.PlayerAttack, StateID.PlayerAttack);
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
        Debug.Log("Player Focus");
        Decision<PlayerController>(player);
    }

    public override void Decision<PlayerController>(PlayerController player)
    {
        Debug.Log("Reasons Focus");
    }
}
