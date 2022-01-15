using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using GameKit;
namespace GameKit
{
    public class ScenesManager : SingletonBase<ScenesManager>
    {
        // 场景加载 同步
        public Scene GetScene()
        {
            return SceneManager.GetActiveScene();
        }
        public void LoadScene(string name, UnityAction callback)
        {
            SceneManager.LoadScene(name);
            callback.Invoke();
        }

        public void LoadSceneAsyn(string name, UnityAction callback)
        {
            MonoManager.instance.StartCoroutine(LoadSceneAsynIE(name, callback));
        }

        public void LoadSceneAsynAdd(string name, UnityAction callback)
        {
            MonoManager.instance.StartCoroutine(LoadSceneAdd(name, callback));
        }

        public void UnloadSceneAsyn(string name, UnityAction callback)
        {
            MonoManager.instance.StartCoroutine(UnloadSceneAsyncIE(name, callback));
        }

        public void TryGetScene(string name, out Scene scene)
        {
            scene = SceneManager.GetSceneByName(name);
        }

        IEnumerator LoadSceneAsynIE(string name, UnityAction callback)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);

            while (!ao.isDone)
            {
                EventManager.instance.EventTrigger("Loading Scene", ao.progress);
                yield return ao.progress;
            }
            callback?.Invoke();
        }

        IEnumerator LoadSceneAdd(string name, UnityAction callback)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            while (!ao.isDone)
            {
                EventManager.instance.EventTrigger("Loading Scene", ao.progress);
                yield return ao.progress;
            }
            callback?.Invoke();
        }

        IEnumerator UnloadSceneAsyncIE(string name, UnityAction callback)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(name);
            while (!ao.isDone)
            {
                EventManager.instance.EventTrigger("Removing Scene", ao.progress);
                yield return ao.progress;
            }
            callback?.Invoke();
        }
    }
}
