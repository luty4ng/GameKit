using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using GameKit;

public class Addressable_Test : MonoBehaviour
{
    public List<GameObject> objs;
    void Start()
    {
        AddressableManager.instance.GetAssetAsyn<GameObject>("Assets/_Addressable/Prefabs/Capsule.prefab", (obj) =>
        {
            Debug.Log(obj.name);
            Instantiate(obj);
            obj.transform.position = Vector3.zero;
        });

        AddressableManager.instance.GetAssetsAsyn<GameObject>(new List<string> { "Prefab" }, (obj) =>
        {
            if (obj is GameObject)
                Debug.Log(obj);
        }, (IList<GameObject> objList) =>
        {
            objs = new List<GameObject>(objList);
        });
    }
}
