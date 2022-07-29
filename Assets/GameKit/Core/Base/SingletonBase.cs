using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace GameKit
{
    public abstract class SingletonBase<T> : IManager where T : new()
    {

        protected static bool IsDisabled = true;
        private static T Instance;
        public static T instance
        {
            get
            {
                if (!IsDisabled)
                {
                    Debug.LogError(Utility.Text.Format("Try To Access Disabled {0} Manager", Instance.ToString()));
                    return default(T);
                }

                if (Instance == null)
                {
                    Instance = new T();
                }
                return Instance;
            }
        }

        private bool isActive = true;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        public virtual void ShutDown()
        {
            Disable();
            IsDisabled = true;
        }

        public virtual void Enable() => isActive = true;
        public virtual void Disable() => isActive = false;
        public virtual void Clear() { }

    }
}


