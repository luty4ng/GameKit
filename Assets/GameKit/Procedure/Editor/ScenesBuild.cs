#if UNITY_EDITOR

using UnityEngine;
using System.IO;
using UnityEditor;

[CustomEditor(typeof(ScenesBuild))]
public class ScenesBuild : Editor
{
    [MenuItem(ScenesConfig.EDITOR_TITLE + "ScenesBuild-All")]
    static void BuildAllScene()
    {
        string[] files = AssetDatabase.FindAssets("t:Scene", new string[] { ScenesConfig.ROOT_PATH });
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length];
        for (int i = 0; i < files.Length; ++i)
        {
            files[i] = AssetDatabase.GUIDToAssetPath(files[i]);
            scenes[i] = new EditorBuildSettingsScene(files[i], true);
            if (i > 0 && Path.GetFileNameWithoutExtension(files[i]) == ScenesConfig.LAUNCHER_NAME)
            {
                var temp = scenes[0];
                scenes[0] = scenes[i];
                scenes[i] = temp;
            }
        }
        EditorBuildSettings.scenes = scenes;
    }

    [MenuItem(ScenesConfig.EDITOR_TITLE + "ScenesBuild-Main")]
    static void BuildMainScene()
    {
        string[] files = AssetDatabase.FindAssets("t:Scene", new string[] { ScenesConfig.GAMEMAIN_PATH });
        EditorBuildSettingsScene[] scenes = new EditorBuildSettingsScene[files.Length];
        for (int i = 0; i < files.Length; ++i)
        {
            files[i] = AssetDatabase.GUIDToAssetPath(files[i]);
            scenes[i] = new EditorBuildSettingsScene(files[i], true);
            if (i > 0 && Path.GetFileNameWithoutExtension(files[i]) == ScenesConfig.LAUNCHER_NAME)
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