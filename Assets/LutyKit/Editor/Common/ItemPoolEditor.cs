using UnityEngine;
using UnityEditor;

// [CustomEditor(typeof(ItemPool))]
public class ItemPoolEditor : Editor
{
    // public override void OnInspectorGUI()
    // {
    //     base.OnInspectorGUI();
    //     ItemPool pool = (ItemPool)target;

    //     if (GUILayout.Button("Update"))
    //     {
    //         Debug.Log("Update Item Pool.");
    //         pool.ClearPool();
    //         pool.AddPrefab("ItemSO/ItemData/Empty", false);
    //         pool.AddPrefab("ItemSO/ItemData");
    //     }
    // }
}