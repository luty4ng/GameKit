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


    [MenuItem(ScenesConfig.EDITOR_TITLE + "ScenesUpdate-All")]
    public static void UpdateScenesAll()
    {
        string scenesMenuPath = Path.Combine(ScenesConfig.ROOT_PATH, ScenesConfig.SCRIPT_PATH);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("#if UNITY_EDITOR");
        stringBuilder.AppendLine("using UnityEditor;");
        stringBuilder.AppendLine("using UnityEditor.SceneManagement;");
        stringBuilder.AppendLine("public static class ScenesList");
        stringBuilder.AppendLine("{");

        foreach (string sceneGuid in AssetDatabase.FindAssets("t:Scene", new string[] { ScenesConfig.ROOT_PATH }))
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            string methodName = scenePath.Replace('/', '_').Replace('\\', '_').Replace('.', '_').Replace('-', '_').Replace(' ', '_');
            stringBuilder.AppendLine(string.Format("    [MenuItem(\"Scenes/{0}\")]", sceneName));
            stringBuilder.AppendLine(string.Format("    public static void {0}() {{ ScenesUpdate.OpenScene(\"{1}\"); }}", methodName, scenePath));
        }
        stringBuilder.AppendLine("}");
        stringBuilder.AppendLine("#endif");
        Directory.CreateDirectory(Path.GetDirectoryName(ScenesConfig.ROOT_PATH + ScenesConfig.SCRIPT_PATH));
        File.WriteAllText(scenesMenuPath, stringBuilder.ToString());
        AssetDatabase.Refresh();
    }

    [MenuItem(ScenesConfig.EDITOR_TITLE + "ScenesUpdate-Main")]
    public static void UpdateScenesMain()
    {
        string scenesMenuPath = Path.Combine(ScenesConfig.ROOT_PATH, ScenesConfig.SCRIPT_PATH);
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("#if UNITY_EDITOR");
        stringBuilder.AppendLine("using UnityEditor;");
        stringBuilder.AppendLine("using UnityEditor.SceneManagement;");
        stringBuilder.AppendLine("public static class ScenesList");
        stringBuilder.AppendLine("{");

        foreach (string sceneGuid in AssetDatabase.FindAssets("t:Scene", new string[] { ScenesConfig.GAMEMAIN_PATH }))
        {
            string scenePath = AssetDatabase.GUIDToAssetPath(sceneGuid);
            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            string methodName = scenePath.Replace('/', '_').Replace('\\', '_').Replace('.', '_').Replace('-', '_').Replace(' ', '_');
            if (sceneName.Equals(ScenesConfig.LAUNCHER_NAME))
                stringBuilder.AppendLine(string.Format("    [MenuItem(\"Scenes/{0}\", false, 0)]", sceneName));
            else
                stringBuilder.AppendLine(string.Format("    [MenuItem(\"Scenes/{0}\")]", sceneName));
            stringBuilder.AppendLine(string.Format("    public static void {0}() {{ ScenesUpdate.OpenScene(\"{1}\"); }}", methodName, scenePath));
        }
        stringBuilder.AppendLine("}");
        stringBuilder.AppendLine("#endif");
        Directory.CreateDirectory(Path.GetDirectoryName(ScenesConfig.ROOT_PATH + ScenesConfig.SCRIPT_PATH));
        File.WriteAllText(scenesMenuPath, stringBuilder.ToString());
        AssetDatabase.Refresh();
    }

    public static void OpenScene(string filename)
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            Debug.Log("Open Scene: " + filename);
            if (EditorSceneManager.sceneCount > 0)
            {
                for (int i = 0; i < EditorSceneManager.sceneCount; i++)
                {
                    EditorSceneManager.UnloadSceneAsync(EditorSceneManager.GetSceneAt(i));
                }
            }
            EditorSceneManager.OpenScene(ScenesConfig.LAUNCHER_PATH + ScenesConfig.LAUNCHER_NAME + ".unity", OpenSceneMode.Single);
            EditorSceneManager.OpenScene(filename, OpenSceneMode.Additive);
        }

    }
}
#endif