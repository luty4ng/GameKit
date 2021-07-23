using UnityEngine;
using UnityEditor;

// [CustomEditor(typeof(PrefabPool))]
// public class PrefabPoolEditor : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         PrefabPool pool = (PrefabPool)target;

//         if(GUILayout.Button("Update"))
//         {
//             Debug.Log("Update Prefab Pool.");
//             pool.ClearPool();
//             pool.AddPrefab("Prefabs/Building/Build_Res");
//             // pool.AddPrefab("Prefabs/Enemy");
//             // pool.AddPrefab("Prefabs/Bullet");
//         }
//     }
// }