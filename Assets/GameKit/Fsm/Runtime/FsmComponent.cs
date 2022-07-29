using GameKit;
using GameKit.Fsm;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Fsm")]
    public sealed class FsmComponent : GameKitComponent
    {
        private IFsmManager m_FsmManager = null;

        public int Count
        {
            get
            {
                return m_FsmManager.Count;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            m_FsmManager = GameKitModuleCenter.GetModule<IFsmManager>();
            if (m_FsmManager == null)
            {
                Debug.LogWarning("FSM manager is invalid.");
                return;
            }
        }
        
        public bool HasFsm<T>() where T : class
        {
            return m_FsmManager.HasFsm<T>();
        }

        public bool HasFsm(Type ownerType)
        {
            return m_FsmManager.HasFsm(ownerType);
        }

        public bool HasFsm<T>(string name) where T : class
        {
            return m_FsmManager.HasFsm<T>(name);
        }

        public bool HasFsm(Type ownerType, string name)
        {
            return m_FsmManager.HasFsm(ownerType, name);
        }

        public IFsm<T> GetFsm<T>() where T : class
        {
            return m_FsmManager.GetFsm<T>();
        }

        public FsmBase GetFsm(Type ownerType)
        {
            return m_FsmManager.GetFsm(ownerType);
        }

        public IFsm<T> GetFsm<T>(string name) where T : class
        {
            return m_FsmManager.GetFsm<T>(name);
        }

        public FsmBase GetFsm(Type ownerType, string name)
        {
            return m_FsmManager.GetFsm(ownerType, name);
        }

        public FsmBase[] GetAllFsms()
        {
            return m_FsmManager.GetAllFsms();
        }

        public void GetAllFsms(List<FsmBase> results)
        {
            m_FsmManager.GetAllFsms(results);
        }

        public IFsm<T> CreateFsm<T>(T owner, params FsmState<T>[] states) where T : class
        {
            return m_FsmManager.CreateFsm(owner, states);
        }

        public IFsm<T> CreateFsm<T>(string name, T owner, params FsmState<T>[] states) where T : class
        {
            return m_FsmManager.CreateFsm(name, owner, states);
        }

        public IFsm<T> CreateFsm<T>(T owner, List<FsmState<T>> states) where T : class
        {
            return m_FsmManager.CreateFsm(owner, states);
        }

        public IFsm<T> CreateFsm<T>(string name, T owner, List<FsmState<T>> states) where T : class
        {
            return m_FsmManager.CreateFsm(name, owner, states);
        }

        public bool DestroyFsm<T>() where T : class
        {
            return m_FsmManager.DestroyFsm<T>();
        }

        public bool DestroyFsm(Type ownerType)
        {
            return m_FsmManager.DestroyFsm(ownerType);
        }

        public bool DestroyFsm<T>(string name) where T : class
        {
            return m_FsmManager.DestroyFsm<T>(name);
        }

        public bool DestroyFsm(Type ownerType, string name)
        {
            return m_FsmManager.DestroyFsm(ownerType, name);
        }

        public bool DestroyFsm<T>(IFsm<T> fsm) where T : class
        {
            return m_FsmManager.DestroyFsm(fsm);
        }

        public bool DestroyFsm(FsmBase fsm)
        {
            return m_FsmManager.DestroyFsm(fsm);
        }
    }
}
