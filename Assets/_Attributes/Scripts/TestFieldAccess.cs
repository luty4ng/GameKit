using UnityEngine;
using System.Reflection;

public class TestFieldAccess : MonoBehaviour
{
    private void Start()
    {
        System.Type type = typeof(TestFieldUserA);
        Assembly testFieldAssembly = Assembly.GetAssembly(type);
        Debug.Log(testFieldAssembly.GetName().Name);

        System.Type[] types =  testFieldAssembly.GetTypes();
        foreach (var t in types)
        {
            
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);
            foreach (var attr in attrs)
            {
                if(attr is TestFieldAttribute)     
                {
                    Debug.Log(t.Name);
                    TestFieldAttribute TFA = (TestFieldAttribute)attr;
                    Debug.Log(attr.ToString());
                }           
            }
        }
        // foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
        // {
        //     // if (asm.GetName() == "TestField")
        //     //     Debug.Log("fuck");
        //     Debug.Log(asm.GetName().Name);
        // }
    }
}