using UnityEngine;
using System;
namespace MyDOTween
{
    internal static class PluginManager
    {
        internal static TweenPlugin<TweenVT, StoreVT> GetDefaultPlugin<TweenVT, StoreVT>()
        {
            Type tweenVT = typeof(TweenVT);
            Type storeVT = typeof(StoreVT);

            ITweenPlugin plugin = null;
            if (tweenVT == typeof(Vector3) && storeVT == typeof(Vector3))
            {
                plugin = new Vector3Plugin();
            }
            else if (tweenVT == typeof(Color) && storeVT == typeof(Color))
            {
                plugin = new ColorPlugin();
            }

            if (plugin != null)
            {
                return plugin as TweenPlugin<TweenVT, StoreVT>;
            }
            else
            {
                return null;
            }
        }
    }
}
