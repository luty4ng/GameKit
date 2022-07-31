using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GameKit
{
    public static partial class SystemExtension
    {
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action)
        {
            if (action == null || dictionary.Count == 0) return;
            for (int i = 0; i < dictionary.Count; i++)
            {
                var item = dictionary.ElementAt(i);
                action(item);
            }
        }
    }
}
