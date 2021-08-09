using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
public class AssetManager : BaseManager<AudioManager>
{
    public List<AssetReference> assetList;

    public static void LoadAssetAsyn<T>(string name, System.Action<T> callback = null)
    {
        // Addressables.LoadAssetsAsync<T>(new string[] { "key1"}, callback, Addressables.MergeMode.);

    }

    public AssetManager()
    {
        // Addressables.LoadAssetsAsync<AssetReference>(new string[] { "key1"}, callback, Addressables.MergeMode.);
    }

}