using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObj : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("Push", 1);
    }

    void Push()
    {
        PoolManager.GetInstance().PushObj(this.gameObject.name, this.gameObject);
    }
}
