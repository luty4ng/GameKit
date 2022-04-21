using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using GameKit;
public enum SceneLoad
{
    Next,
    Previous
}

[CreateAssetMenu(fileName = "SceneCollection", menuName = "GameKit/SceneCollection", order = 0)]
public class SceneCollection : ScriptableObject
{
    public List<SceneData> collections;
    public int currentIndex = 0;
    public void LoadNextScene(UnityAction callback = null)
    {
        ScenesManager.instance.UnloadSceneAsyn(collections[currentIndex].name, () =>
        {
            currentIndex = (currentIndex + 1) % collections.Count;
            ScenesManager.instance.LoadSceneAsynAdd(collections[currentIndex].name, () =>
            {
                callback?.Invoke();
            });
        });

    }
    public void LoadPreviousScene(UnityAction callback = null)
    {
        ScenesManager.instance.UnloadSceneAsyn(collections[currentIndex].name, () =>
        {
            currentIndex = currentIndex <= 0 ? collections.Count - 1 : (currentIndex - 1) % collections.Count;
            ScenesManager.instance.LoadSceneAsynAdd(collections[currentIndex].name, () =>
            {
                callback?.Invoke();
            });
        });

    }
    public void LoadDefaultScene()
    {
        currentIndex = 0;
        ScenesManager.instance.LoadSceneAsynAdd(collections[0].name, () => { });
    }

    IEnumerator LoadSceneIE(SceneLoad loadMode, UnityAction beforeRemove, UnityAction afterLoad)
    {
        // 触发关卡去除动画

        yield return new WaitUntil(CheckAnimOver());
        ScenesManager.instance.UnloadSceneAsyn(collections[currentIndex].name, () =>
        {
            if (loadMode == SceneLoad.Next)
                currentIndex = (currentIndex + 1) % collections.Count;
            else
                currentIndex = currentIndex <= 0 ? collections.Count - 1 : (currentIndex - 1) % collections.Count;
            ScenesManager.instance.LoadSceneAsynAdd(collections[currentIndex].name, () =>
            {
                // 触发关卡生成动画
                afterLoad?.Invoke();
            });
        });
    }

    System.Func<bool> CheckAnimOver() => () => false;
}