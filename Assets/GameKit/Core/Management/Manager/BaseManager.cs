using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace GameKit
{
    public interface IBaseManager { }
    public class BaseManager<T> : IBaseManager where T : new()
    {
        private static T Instance;
        public static T instance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new T();
                    CoreManager.RegisterManager(Instance as IBaseManager);
                }
                return Instance;
            }
        }
    }
}


