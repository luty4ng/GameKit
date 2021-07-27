using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Addressable_Test : MonoBehaviour
{
    void Start()
    {
        Addressables.LoadAssetAsync<Material>("Assets/Addressable_Demo/Prefabs/Capsule.prefab[Assets/Arts/Addressable/Black 1.mat]");
    }


}
