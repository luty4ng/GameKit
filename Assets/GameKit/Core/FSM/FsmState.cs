using System;

namespace GameKit.Fsm
{
    public abstract class FsmState<T> where T : class
    {
        public FsmState() { }
        protected virtual internal void OnInit(IFsm<T> fsm) { }
        protected virtual internal void OnEnter(IFsm<T> fsm) { }
        protected virtual internal void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds) { }
        protected virtual internal void OnExit(IFsm<T> fsm, bool isShutdown) { }
        protected virtual internal void OnDestroy(IFsm<T> fsm) { }

        protected void ChangeState<TState>(IFsm<T> fsm) where TState : FsmState<T>
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new GameKitException("FSM is invalid.");
            }

            fsmImplement.ChangeState<TState>();
        }

        protected void ChangeState(IFsm<T> fsm, Type stateType)
        {
            Fsm<T> fsmImplement = (Fsm<T>)fsm;
            if (fsmImplement == null)
            {
                throw new GameKitException("FSM is invalid.");
            }

            if (stateType == null)
            {
                throw new GameKitException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new GameKitException(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            fsmImplement.ChangeState(stateType);
        }
    }
}
