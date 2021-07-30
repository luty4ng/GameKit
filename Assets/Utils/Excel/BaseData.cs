using UnityEngine;
using Sirenix.OdinInspector;

public class BaseData : SerializedScriptableObject
{
    [LabelText("名称（唯一ID）")] public string idName;
}