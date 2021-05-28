using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM.SO
{
    public enum Transition
    {
        NUllTransition = 0,
        PlayerMove,
        PlayerAttack,
        PlayerFocus
    }

    public enum StateID
    {
        NullState = 0,
        PlayerMove,
        PlayerAttack,
        PlayerFocus
    }

    public abstract class States
    {
        public Dictionary<Transition, StateID> trans = new Dictionary<Transition, StateID>();
        //public readonly State stateID;
        protected StateID stateID;                     // 状态ID  
        public StateID stateid { get { return stateID; } }
        //  可添加对外部的引用，以保证子状态能够访问，例如 GameManager

        public void AddTransition(Transition target, StateID id)
        {
            if (target == Transition.NUllTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed for a real transition");
                return;
            }

            if (id == StateID.NullState)
            {
                Debug.LogError("FSMState ERROR: NullStateID is not allowed for a real ID");
                return;
            }

            if (trans.ContainsKey(target))
            {
                Debug.LogError("FSMState ERROR: State " + stateID.ToString() + " already has transition " + trans.ToString() +
                               "Impossible to assign to another state");
                return;
            }

            trans.Add(target, id);
        }

        public void DeleteTransition(Transition target)
        {
            if (target == Transition.NUllTransition)
            {
                Debug.LogError("FSMState ERROR: NullTransition is not allowed");
                return;
            }

            if (trans.ContainsKey(target))
            {
                trans.Remove(target);
                return;
            }

            Debug.LogError("FSMState ERROR: Transition " + trans.ToString() + " passed to " + stateID.ToString() +
                           " was not on the state's transition list");
        }

        public StateID GetState(Transition target)
        {
            if (trans.ContainsKey(target))
            {
                return trans[target];
            }
            return StateID.NullState;
        }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }
        public abstract void Excute<T>(T target);
        public abstract void Decision<T>(T target);

    }

}
