using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SceneData", menuName = "GameKit/SceneData", order = 0)]
public class SceneData : ScriptableObject {
    [Header("Information")]
    public string sceneName;
    [TextArea]
    public string sceneDesc;    
}