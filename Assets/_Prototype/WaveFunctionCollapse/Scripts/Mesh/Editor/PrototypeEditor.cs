using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Prototype))]
public class PrototypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        Prototype prototype = (Prototype)target;
        if(GUILayout.Button("SaveConfig"))
        {
            prototype.SaveConfig();
        }
    }
}