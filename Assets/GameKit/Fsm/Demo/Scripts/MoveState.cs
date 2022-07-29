using UnityEngine;
using GameKit.Fsm;
using FsmInterface = GameKit.Fsm.IFsm<GameKit.Demo.FsmPlayer>;

namespace GameKit.Demo
{
    public class MoveState : FsmState<FsmPlayer>, IReference
    {
        private FsmPlayer user;
        public void Clear()
        {

        }

        protected internal override void OnEnter(FsmInterface ifsm)
        {
            base.OnEnter(ifsm);
            Debug.Log("Enter Move State.");
            user = ifsm.User;
        }

        protected internal override void OnUpdate(FsmInterface ifsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(ifsm, elapseSeconds, realElapseSeconds);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState<IdleState>(ifsm);
            }
        }
    }
}
