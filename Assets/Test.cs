using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum TestType
{
    A,
    BBBB,
    C
}
public class Test : MonoBehaviour
{
    [Tooltip("移动速度")] public float speed;
    void Start()
    {
        
    }
}
