#if UNITY_EDITOR

using UnityEngine;
using System.IO;
using UnityEditor;

[CustomEditor(typeof(ScenesBuild))]
public class ScenesBuild : Editor
{
    [MenuItem("SceneManagement/ScenesBuild-Main")]
    static void BuildMainScene()
    {
        string[] files = AssetDatabase.FindAssets("t:Scene", new string[] { "Assets/GameMain/Scenes" });
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length];
        for (int i = 0; i < files.Length; ++i)
        {
            files[i] = AssetDatabase.GUIDToAssetPath(files[i]);
            scenes[i] = new EditorBuildSettingsScene(files[i], true);
            if (i > 0 && Path.GetFileNameWithoutExtension(files[i]) == ScenesConfig.procedureScene)
            {
                var temp = scenes[0];
                scenes[0] = scenes[i];
                scenes[i] = temp;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }

    [MenuItem("SceneManagement/ScenesBuild-All")]
    static void BuildAllScene()
    {
        string[] files = AssetDatabase.FindAssets("t:Scene", new string[] { "Assets" });
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length];
        for (int i = 0; i < files.Length; ++i)
        {
            files[i] = AssetDatabase.GUIDToAssetPath(files[i]);
            scenes[i] = new EditorBuildSettingsScene(files[i], true);
            if (i > 0 && Path.GetFileNameWithoutExtension(files[i]) ==  ScenesConfig.procedureScene)
            {
                var temp = scenes[0];
                scenes[0] = scenes[i];
                scenes[i] = temp;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }
}

#endif