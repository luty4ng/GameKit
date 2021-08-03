using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ManagerDebug))]
public class ManagerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
    }
    
}

public class ManagerWindow : EditorWindow
{
    [MenuItem("MyTools/ManagerDebug")]
    public static void DisplayPreviewWindow()
    {
        EditorWindow.GetWindow(typeof(ManagerWindow));
    }

    private void Awake()
    {
        titleContent.text = "Manager Debugger";
    } 

    private void OnGUI()
    {
        
    }
}