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

    public void LoadAsync<T>(string name, UnityAction<T> callback) where T : Object
    {
        MonoManager.GetInstance().StartCoroutine(LoadAsyncProcess<T>(name, callback));
    }

    private IEnumerator LoadAsyncProcess<T>(string name, UnityAction<T> callback) where T : Object
    {
        ResourceRequest res = Resources.LoadAsync<T>(name);
        yield return res;

        if(res.asset is GameObject)
            callback.Invoke(GameObject.Instantiate(res.asset) as T);
        else
            callback.Invoke(res.asset as T);
        
        // 这里的 Callback.Invoke 用法类似于 return
        // 委托说白了就是暂未指定实参的函数集合
    }
}
