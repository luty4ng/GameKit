using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : BaseManager<MonoManager>
{
    public MonoController controller;

    public MonoManager()
    {
        GameObject obj = new GameObject("MonoController");
        controller = obj.AddComponent<MonoController>();
        Debug.Log("创建成功！");
    }

    public void AddUpdateListener(UnityAction func)
    {
        controller.AddUpdateListener(func);
    }

    public void RomoveUpdateListener(UnityAction func)
    {
        controller.RemoveUpdateListener(func);
    }

    public Coroutine StartCoroutine(IEnumerator routine)
    {
        return controller.StartCoroutine(routine);
    }

    public Coroutine StartCoroutine(string methodName)
    {
        return controller.StartCoroutine(methodName);
    }
}
