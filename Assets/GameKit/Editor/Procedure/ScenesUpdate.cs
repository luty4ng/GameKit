#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class ScenesUpdate
{
    static readonly string scriptPath = "GameKit/Editor/Procedure/ScenesList.cs";

    [MenuItem("SceneManagement/ScenesUpdate")]
    public static void UpdateList()
    {
        string scenesMenuPath = Path.Combine(Application.dataPath, scriptPath);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("#if UNITY_EDITOR");
        stringBuilder.AppendLine("using UnityEditor;");
        stringBuilder.AppendLine("using UnityEditor.SceneManagement;");
        stringBuilder.AppendLine("public static class ScenesList");
        stringBuilder.AppendLine("{");

        foreach (string sceneGuid in AssetDatabase.FindAssets("t:Scene", new string[] { "Assets" }))
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            string methodName = scenePath.Replace('/', '_').Replace('\\', '_').Replace('.', '_').Replace('-', '_').Replace(' ', '_');
            stringBuilder.AppendLine(string.Format("    [MenuItem(\"Scenes/{0}\")]", sceneName));
            stringBuilder.AppendLine(string.Format("    public static void {0}() {{ ScenesUpdate.OpenScene(\"{1}\"); }}", methodName, scenePath));
        }
        stringBuilder.AppendLine("}");
        stringBuilder.AppendLine("#endif");
        Directory.CreateDirectory(Path.GetDirectoryName(scriptPath));
        File.WriteAllText(scenesMenuPath, stringBuilder.ToString());
        AssetDatabase.Refresh();
    }

    public static void OpenScene(string filename)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            Debug.Log(filename);
            if (EditorSceneManager.sceneCount > 0)
            {
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneAt(i));
                }
            }
            EditorSceneManager.OpenScene("Assets/GameMain/Scenes/" + ScenesConfig.procedureScene + ".unity", OpenSceneMode.Single);
            EditorSceneManager.OpenScene(filename, OpenSceneMode.Additive);
        }

    }
}
#endif