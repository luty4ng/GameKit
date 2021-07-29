using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Addressable_Test : MonoBehaviour
{
    public AssetReference mat;
    void Start()
    {
        AsyncOperationHandle handle = Addressables.LoadAssetAsync<GameObject>("Assets/Addressable_Demo/Prefabs/Capsule.prefab");
        handle.Completed += test;
        // reference.
    }

    void test(AsyncOperationHandle obj)
    {

    }   
}
