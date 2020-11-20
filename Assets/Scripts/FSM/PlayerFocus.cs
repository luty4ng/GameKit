using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFocus : States {
    public PlayerFocus()
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
        if(Input.GetKeyDown(KeyCode.W))
        {
            StateManager.GetInstance().PerformTransition(Transition.PlayerAttack);
        }
    }
}
