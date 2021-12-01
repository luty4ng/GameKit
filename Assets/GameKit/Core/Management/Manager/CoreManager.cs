using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameKit;

namespace GameKit
{
    public class CoreManager : MonoBehaviour
    {
        [SerializeField] public static List<IBaseManager> managers;

        public static void RegisterManager(IBaseManager manager)
        {
            if (managers == null)
                managers = new List<IBaseManager>();
            managers.Add(manager);
        }

        public static void RemoveManager(IBaseManager manager)
        {
            if (managers.Contains(manager))
                managers.Remove(manager);
        }

        public static void Clear()
        {
            if(managers != null)
                managers.Clear();
        }
    }
}
