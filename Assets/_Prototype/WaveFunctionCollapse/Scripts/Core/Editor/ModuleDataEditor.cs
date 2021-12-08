using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(ModuleData), true)]
[CanEditMultipleObjects]
public class ModuleDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ModuleData moduleData = (ModuleData)target;
        if (GUILayout.Button("Create Module"))
        {
            moduleData.CreateModules();
        }

        if (GUILayout.Button("Save Module Data"))
        {
            moduleData.SaveConfig();
        }
    }
}