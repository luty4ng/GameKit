using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit.DataStructure;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif
namespace GameKit
{
    public class ResourceManager : SingletonBase<ResourceManager>
    {
        private Dictionary<int, AsyncOperationHandle> m_cachedHandles;

        public ResourceManager()
        {
            m_cachedHandles = new Dictionary<int, AsyncOperationHandle>();
        }
        public T Load<T>(string name) where T : Object
        {
            T res = Resources.Load<T>(name);

            if (res is GameObject)
                return GameObject.Instantiate(res);
            else
                return res;
        }

        public void Load<T>(string name, UnityAction<T> callback) where T : Object
        {
            T res = Resources.Load<T>(name);
            if (res is GameObject)
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
            if (res.asset is GameObject)
                callback.Invoke(GameObject.Instantiate(res.asset) as T);
            else
                callback.Invoke(res.asset as T);
        }

        public T[] LoadPath<T>(string path) where T : Object
        {
            T[] res = Resources.LoadAll<T>(path);
            if (res is GameObject)
                return null;
            else
                return res;
        }

#if PACKAGE_ADDRESSABLES
        IEnumerator GetAsynProcess<T>(string keyName, UnityAction<T> callback) where T : Object
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            yield return handle;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                if (handle.Result is GameObject)
                    callback?.Invoke(GameObject.Instantiate(handle.Result as GameObject) as T);
                else
                    callback?.Invoke(handle.Result as T);

                if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                    m_cachedHandles.Add(handle.Result.GetInstanceID(), handle);
            }
        }

        IEnumerator GetAsynProcessByLabel<T>(IList<string> labels, UnityAction<T> eachCall, UnityAction<IList<T>> callback) where T : Object
        {
            AsyncOperationHandle<IList<T>> handle =
                Addressables.LoadAssetsAsync<T>(labels,
                    obj =>
                    {
                        eachCall?.Invoke(obj as T);
                    }, Addressables.MergeMode.Union, false);

            yield return handle;
            callback?.Invoke(handle.Result as IList<T>);
            if (!m_cachedHandles.ContainsKey(handle.Result.First().GetInstanceID()))
                m_cachedHandles.Add(handle.Result.First().GetInstanceID(), handle);
        }
#endif

        public void GetAssetAsyn<T>(string keyName, UnityAction<T> callback = null) where T : Object
        {
#if PACKAGE_ADDRESSABLES
            MonoManager.instance.StartCoroutine(GetAsynProcess<T>(keyName, callback));
#else
            Utility.Debug.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void GetAssetsAsyn<T>(IList<string> labels, UnityAction<T> eachCall = null, UnityAction<IList<T>> callback = null) where T : Object
        {
#if PACKAGE_ADDRESSABLES
            MonoManager.instance.StartCoroutine(GetAsynProcessByLabel<T>(labels, eachCall, callback));
#else
            Utility.Debug.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void GetAsset<T>(string keyName, UnityAction<T> action) where T : Object
        {
#if PACKAGE_ADDRESSABLES
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(keyName);
            T result = handle.WaitForCompletion();
            action?.Invoke(result);
            if (!m_cachedHandles.ContainsKey(handle.Result.GetInstanceID()))
                m_cachedHandles.Add(handle.Result.GetInstanceID(), handle);
#else
            Utility.Debug.LogFail("Addressables Is Not Installed.");
#endif
        }

        public void GetAssets<T>(IList<string> labels, UnityAction<IList<T>> action) where T : Object
        {
#if PACKAGE_ADDRESSABLES
            AsyncOperationHandle<IList<T>> handle = Addressables.LoadAssetsAsync<T>(labels, null);
            IList<T> result = handle.WaitForCompletion();
            action?.Invoke(result);
            if (!m_cachedHandles.ContainsKey(handle.Result.First().GetInstanceID()))
                m_cachedHandles.Add(handle.Result.First().GetInstanceID(), handle);
#else
            Utility.Debug.LogFail("Addressables Is Not Installed.");
#endif
        }

        public override void Clear()
        {
            base.Clear();
            m_cachedHandles.Clear();
        }

        public override void ShutDown()
        {
            m_cachedHandles.Clear();
            base.ShutDown();
        }

        public void ReleaseHandle(Object obj)
        {
            int instanceId = obj.GetInstanceID();
            if (m_cachedHandles.ContainsKey(instanceId))
            {
                Addressables.Release(m_cachedHandles[instanceId]);
                m_cachedHandles.Remove(instanceId);
            }
            else
                Utility.Debugger.LogWarning("Try Release Uncached {0} Asset Handle.", obj.name);
        }
    }
}
