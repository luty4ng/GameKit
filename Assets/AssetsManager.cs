using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AssetsManager
{
    public List<AssetReference> assetRefer;
    private static AssetsManager Instance;
    public static AssetsManager instance
    {
        get
        {
            if(Instance == null)
                Instance = new AssetsManager();
            return Instance;
        }
    }

    public static void LoadAssetAsyn<T>(string name, System.Action<T> callback = null)
    {
        // Addressables.LoadAssetsAsync<T>(new string[] { "key1"}, callback, Addressables.MergeMode.);

    }   

    public AssetsManager()
    {
        // Addressables.LoadAssetsAsync<AssetReference>(new string[] { "key1"}, callback, Addressables.MergeMode.);
    }

}