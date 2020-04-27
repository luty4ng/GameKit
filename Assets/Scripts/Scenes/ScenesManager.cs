using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class ScenesManager : BaseManager<ScenesManager>
{
    // 场景加载 同步
    public void LoadScene(string name, UnityAction func)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadSceneAsyn(string name, UnityAction func)
    {
        MonoManager.GetInstance().StartCoroutine(LoadSceneAsynIE(name, func));
    }

    IEnumerator LoadSceneAsynIE(string name, UnityAction func)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(name);

        // 持续返回加载进度
        while(!ao.isDone)
        {
            // 分发进度条
            EventCenter.GetInstance().EventTrigger("Loading Scene",ao.progress);
            yield return ao.progress;
        }
        //yield return ao;
        func();
    }
}
