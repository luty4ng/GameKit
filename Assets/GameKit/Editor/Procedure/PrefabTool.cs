#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class PrefabTool : EditorWindow
{
    private GameObject parentNode;
    private string savePath;
    [MenuItem("GameObject/GameKitTools/PrefabTool"), MenuItem("Tools/PrefabTool")]
    private static void ShowWindow()
    {
        var window = GetWindow<PrefabTool>();
        window.titleContent = new GUIContent("PrefabTool");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Prefab Parent:");
        // EditorGUILayout.ObjectField();
    }
}
# endif