using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ResReq
{
    public ItemData data;
    public int num;
}

[CreateAssetMenu(fileName = "Test", menuName = "Test")]
public class TestSO : SerializedScriptableObject
{
    [System.NonSerialized, OdinSerialize]
    public List<ResReq> jojo;
    // [System.NonSerialized, OdinSerialize]
    // public ResReq[] wtf;
}
