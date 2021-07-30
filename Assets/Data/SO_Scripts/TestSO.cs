using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ResReq
{
    [System.NonSerialized, OdinSerialize]
    public ItemData data;
    [System.NonSerialized, OdinSerialize]
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
