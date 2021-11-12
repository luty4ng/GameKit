using UnityEngine;
using System.Reflection;

[TestField("TestClass")]
public class FirstClass
{

}


public class TestFieldAccess : MonoBehaviour
{
    private void Start()
    {
        GetPropertyAttribues();
    }



    private void GetClassAttributes()
    {
        System.Type type = typeof(TestFieldUserA);
        Assembly testFieldAssembly = Assembly.GetAssembly(type);
        // Debug.Log(testFieldAssembly.GetName().Name);

        System.Type[] types =  testFieldAssembly.GetTypes();
        foreach (var t in types)
        {
            
            System.Attribute[] attrs = System.Attribute.GetCustomAttributes(t);
            foreach (var attr in attrs)
            {
                // Debug.Log(t.Name.ToString());
                if(attr is TestFieldAttribute)     
                {
                    // Debug.Log(t.Name);
                    TestFieldAttribute TFA = (TestFieldAttribute)attr;
                    Debug.Log(TFA.GetName());
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

    private void GetPropertyAttribues()
    {
        System.Type type = typeof(TestFieldUserA);
        Assembly testFieldAssembly = Assembly.GetAssembly(type);
        System.Type[] types =  testFieldAssembly.GetTypes();
        foreach (var t in types)
        {
            PropertyInfo[] propInfo = t.GetProperties();
            foreach (var prop in propInfo)
            {
                System.Attribute[] attrs = System.Attribute.GetCustomAttributes(prop);
                foreach (var attr in attrs)
                {
                    Debug.Log(attr.ToString());
                    if(attr is TestFieldAttribute)
                    {
                        TestFieldAttribute TFA = (TestFieldAttribute)attr;
                        Debug.Log((TFA.GetName()));
                    }
                }
            }
        }
    }
}