using System;
using System.Collections.Generic;

namespace GameKit.Fsm
{
    internal sealed class Fsm<T> : FsmBase, IFsm<T>, IReference where T : class
    {
        private T m_User;
        private readonly Dictionary<Type, FsmState<T>> m_States;
        // private Dictionary<string, Variable> m_Datas;
        private FsmState<T> m_CurrentState;
        private float m_CurrentStateTime;
        private bool m_IsDestroyed;

        public Fsm()
        {
            m_User = null;
            m_States = new Dictionary<Type, FsmState<T>>();
            // m_Datas = null;
            m_CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
        }

        public T User
        {
            get
            {
                return m_User;
            }
        }

        public override Type UserType
        {
            get
            {
                return typeof(T);
            }
        }

        public override int FsmStateCount
        {
            get
            {
                return m_States.Count;
            }
        }

        public override bool IsRunning
        {
            get
            {
                return m_CurrentState != null;
            }
        }

        public override bool IsDestroyed
        {
            get
            {
                return m_IsDestroyed;
            }
        }

        public FsmState<T> CurrentState
        {
            get
            {
                return m_CurrentState;
            }
        }

        public override string CurrentStateName
        {
            get
            {
                return m_CurrentState != null ? m_CurrentState.GetType().FullName : null;
            }
        }

        public override float CurrentStateTime
        {
            get
            {
                return m_CurrentStateTime;
            }
        }
        public static Fsm<T> Create(string name, T user, params FsmState<T>[] states)
        {
            if (user == null)
                throw new GameKitException(string.Format("{0} Fsm user is invalid.", name));

            if (states == null || states.Length < 1)
                throw new GameKitException(string.Format("{0} Fsm states is invalid.", name));

            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.m_User = user;
            fsm.m_IsDestroyed = false;
            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    throw new GameKitException("FSM states is invalid.");
                }
                Type stateType = state.GetType();
                if (fsm.m_States.ContainsKey(stateType))
                {
                    throw new GameKitException(string.Format("Fsm '{0}' state '{1}' is already exist.", new TypeNamePair(typeof(T), name), stateType.FullName));
                }

                fsm.m_States.Add(stateType, state);
                state.OnInit(fsm);
            }
            return fsm;
        }

        public static Fsm<T> Create(string name, T user, List<FsmState<T>> states)
        {
            if (user == null)
            {
                throw new GameKitException("FSM owner is invalid.");
            }

            if (states == null || states.Count < 1)
            {
                throw new GameKitException("FSM states is invalid.");
            }

            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.m_User = user;
            fsm.m_IsDestroyed = false;
            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    throw new GameKitException("FSM states is invalid.");
                }

                Type stateType = state.GetType();
                if (fsm.m_States.ContainsKey(stateType))
                {
                    throw new GameKitException(string.Format("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof(T), name), stateType.FullName));
                }

                fsm.m_States.Add(stateType, state);
                state.OnInit(fsm);
            }

            return fsm;
        }
        public void Clear()
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.OnExit(this, true);
            }

            foreach (var state in m_States)
            {
                state.Value.OnDestroy(this);
            }

            Name = null;
            m_User = null;
            m_States.Clear();

            // if (m_Datas != null)
            // {
            //     foreach (KeyValuePair<string, Variable> data in m_Datas)
            //     {
            //         if (data.Value == null)
            //         {
            //             continue;
            //         }

            //         ReferencePool.Release(data.Value);
            //     }

            //     m_Datas.Clear();
            // }

            m_CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
        }

        public void Start<TState>() where TState : FsmState<T>
        {
            if (IsRunning)
            {
                throw new GameKitException(string.Format("{0} Fsm is already running.", Name));
            }

            FsmState<T> state = GetState<TState>();
            if (state == null)
            {
                throw new GameKitException(string.Format("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof(T), Name), typeof(TState).FullName));
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }

        public void Start(Type stateType)
        {
            if (IsRunning)
            {
                throw new GameKitException("FSM is running, can not start again.");
            }

            if (stateType == null)
            {
                throw new GameKitException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new GameKitException(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new GameKitException(string.Format("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof(T), Name), stateType.FullName));
            }

            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }
        public bool HasState<TState>() where TState : FsmState<T>
        {
            return m_States.ContainsKey(typeof(TState));
        }
        public bool HasState(Type stateType)
        {
            if (stateType == null)
            {
                throw new GameKitException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new GameKitException(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            return m_States.ContainsKey(stateType);
        }

        public FsmState<T>[] GetAllStates()
        {
            int index = 0;
            FsmState<T>[] results = new FsmState<T>[m_States.Count];
            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                results[index++] = state.Value;
            }

            return results;
        }

        public void GetAllStates(List<FsmState<T>> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                results.Add(state.Value);
            }
        }

        public TState GetState<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = null;
            if (m_States.TryGetValue(typeof(TState), out state))
            {
                return (TState)state;
            }
            return null;
        }

        public FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new GameKitException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new GameKitException(string.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            FsmState<T> state = null;
            if (m_States.TryGetValue(stateType, out state))
            {
                return state;
            }

            return null;
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapseSeconds;
            m_CurrentState.OnUpdate(this, elapseSeconds, realElapseSeconds);
        }

        internal override void Shutdown()
        {
            ReferencePool.Release(this);
        }

        internal void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        internal void ChangeState(Type stateType)
        {
            if (m_CurrentState == null)
                throw new GameKitException("Current State is invalid.");

            FsmState<T> state = GetState(stateType);

            if (state == null)
                throw new GameKitException(string.Format("FSM '{0}' can not change state to '{1}' which is not exist.", new TypeNamePair(typeof(T), Name), stateType.FullName));

            m_CurrentState.OnExit(this, false);
            m_CurrentStateTime = 0f;
            m_CurrentState = state;
            m_CurrentState.OnEnter(this);
        }

    }
}