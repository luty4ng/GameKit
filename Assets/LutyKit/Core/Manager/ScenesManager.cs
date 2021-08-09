using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesManager : BaseManager<ScenesManager>
{
    // 场景加载 同步
    public Scene GetScene()
    {
        return SceneManager.GetActiveScene();
    }
    public void LoadScene(string name, UnityAction func)
    {
        SceneManager.LoadScene(name);
        func();
    }

    public void LoadSceneAsyn(string name, UnityAction func)
    {
        MonoManager.instance.StartCoroutine(LoadSceneAsynIE(name, func));
    }

    IEnumerator LoadSceneAsynIE(string name, UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);

        // 持续返回加载进度
        while(!ao.isDone)
        {
            // 分发进度条，UI管理器监听该条信息
            EventCenter.instance.EventTrigger("Loading Scene", ao.progress);
            yield return ao.progress;
        }
        //yield return ao;
        func();
    }
}
