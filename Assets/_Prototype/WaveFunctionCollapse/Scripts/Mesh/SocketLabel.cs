using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SocketLabel : MonoBehaviour
{
    Prototype[] prototypes;
    private void Start()
    {
        prototypes = GetComponentsInChildren<Prototype>();
    }


}


# if UNITY_EDITOR
[CustomEditor(typeof(SocketLabel))]
public class SocketLabelEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SocketLabel socketLabel = (SocketLabel)target;
        if (GUILayout.Button("Label Socket"))
        {

        }
    }
}
#endif