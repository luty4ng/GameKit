using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace GameKit
{
    public abstract class SingletonBase<T> where T : new()
    {
        private static T Instance;
        public static T instance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new T();
                }
                return Instance;
            }
        }
    }
}


