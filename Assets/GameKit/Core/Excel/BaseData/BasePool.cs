using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class BasePool<T> : ScriptableObject where T : BaseData
{
    public List<T> pool;
    public T GetAsset(string name)
    {
        if(pool != null && pool.Count > 0)
        {
            return pool.FirstOrDefault(item => item.idName == name);
        }
        return null;
    }
}
