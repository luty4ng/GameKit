using UnityEngine;
using GameKit.Fsm;
using FsmInterface = GameKit.Fsm.IFsm<GameKit.Demo.FsmPlayer>;

namespace GameKit.Demo
{
    public class IdleState : FsmState<FsmPlayer>, IReference
    {
        private FsmPlayer user;
        public void Clear()
        {

        }

        protected internal override void OnEnter(FsmInterface updateFsm)
        {
            base.OnEnter(updateFsm);
            Debug.Log("Enter Idle State.");
            user = updateFsm.User;
        }

        protected internal override void OnUpdate(FsmInterface ifsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(ifsm, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState<MoveState>(ifsm);
            }
        }
    }
}
