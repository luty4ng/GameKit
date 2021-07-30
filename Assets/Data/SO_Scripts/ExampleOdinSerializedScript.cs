using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using UnityEngine;


public class ExampleOdinSerializedScript : SerializedMonoBehaviour
{
    private void Start()
    {
        // TestSO testSO = Resources.Load<TestSO>("TestSO");
        // ES3.Save("TestDataSO", testSO);

        // // TestSOB testSOB = Resources.Load<TestSOB>("TestSOB");
        // // ES3.Save("TestDataSOB", testSOB);
        // TestSO testS = ScriptableObject.CreateInstance<TestSO>();
        // testS = ES3.Load<TestSO>("TestDataSO");
        // Debug.Log(testS.jojo[0].num);
    }
}
