using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ResourceManager : BaseManager<ResourceManager>
{
    
    public T Load<T>(string name) where T : Object
    {
        T res = Resources.Load<T>(name);
        
        if(res is GameObject)
            return GameObject.Instantiate(res);
        else
            return res;
    }

    public void Load<T>(string name, UnityAction<T> callback) where T : Object
    {
        T res = Resources.Load<T>(name);
        if(res is GameObject)
            callback.Invoke(GameObject.Instantiate(res as T));
        else
            callback.Invoke(res as T);
    }

    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        MonoManager.instance.StartCoroutine(LoadAsyncProcess<T>(name, callback));
    }

    private IEnumerator LoadAsyncProcess<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest res = Resources.LoadAsync<T>(name);
        yield return res;
    
        if(res.asset is GameObject)
            callback.Invoke(GameObject.Instantiate(res.asset) as T);
        else
            callback.Invoke(res.asset as T);
    }

    public T[] LoadPath<T>(string path) where T : Object
    {
        T[] res = Resources.LoadAll<T>(path);
        if(res is GameObject)
            return null;
        else
            return res;
    }
}
