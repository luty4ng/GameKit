using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public string namea = " good";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 测试缓存池
        if(Input.GetKeyDown(KeyCode.Z))
        {
            PoolManager.GetInstance().GetObj("Prefabs/Circle");
        }

        // 测试公共Update
        if(Input.GetKeyDown(KeyCode.X))
        {
            MonoManager.GetInstance().AddUpdateListener(KeepUpdate);
        }
        
        // 测试公共协程
        if(Input.GetKeyDown(KeyCode.C))
        {
            MonoManager.GetInstance().StartCoroutine(testMono());
        }
        
        // 测试事件中心
        if(Input.GetKeyDown(KeyCode.D))
        {
            DoIt();
        }

        
    }

    void KeepUpdate()
    {
        Debug.Log("Keep Updating");
    }

    IEnumerator testMono()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("MonoManager Startcoroutine testing");
    }

    void DoIt()
    {
        Debug.Log("Begin Do it");
        EventCenter.GetInstance().EventTrigger("DoIt", this);
    }
}
