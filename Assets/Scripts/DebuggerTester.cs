using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerTester : MonoBehaviour
{
    // Start is called before the first frame update
    public float A = 1;
    public float B = 2;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("开始测试：");
            TestClassB tmpB = new TestClassB();
            float C = A + B;
            float D = 20;
            float result = C + D + tmpB.GetValue();
            Debug.Log(result);
        }
    }
}

public class TestClassA
{
    protected float A_Value = 10;
}

public class TestClassB : TestClassA
{
    public float GetValue()
    {
        return A_Value;
    }
}