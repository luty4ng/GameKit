using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;

namespace GameKit
{
    public delegate void MonoAction<T>(T param);
    public delegate void MonoAction<T0, T1>(T0 param1, T1 param2);
    
    public class GameKitMonoCenter : MonoBehaviour
    {
        private event UnityAction updateEvent;
        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        void Update()
        {
            if (updateEvent != null)
                updateEvent();
        }

        public void AddUpdateListener(UnityAction func)
        {
            updateEvent += func;
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            updateEvent -= func;
        }
    }
}