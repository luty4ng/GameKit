using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace GameKit
{
    public abstract class SingletonMonoBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T current { get; private set; }
        private void Awake()
        {
            if (current == null)
                current = (this as T);
            OnAwake();
        }
        protected abstract void OnAwake();

    }
}


