using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

public struct ExposedFieldInfo
{
    public MemberInfo memberInfo;
    public ExposedFieldAttribute attribute;
    public System.Type type;

    public ExposedFieldInfo(MemberInfo member, ExposedFieldAttribute attr, System.Type newType)
    {
        memberInfo = member;
        attribute = attr;
        type = newType;
    }

}

[CustomEditor(typeof(ManagerDebug))]
public class ManagerDebug : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }
}

public class ManagerWindow : EditorWindow
{
    [MenuItem("MyTools/ManagerDebug")]
    public static void OpenwWindow()
    {
        // EditorWindow.GetWindow(typeof(ManagerWindow));
        ManagerWindow window = EditorWindow.CreateWindow<ManagerWindow>("Manager Debbger", typeof(ManagerWindow));
    }

    private List<ExposedFieldInfo> exposedMembers = new List<ExposedFieldInfo>();

    private void OnEnable()
    {
        Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        foreach (var assembly in assemblies)
        {
            System.Type[] types = assembly.GetTypes();

            foreach (System.Type type in types)
            {
                if (type.BaseType == null)
                    continue;
                if (type.BaseType.Name.Equals("BaseManager`1"))
                {
                    MemberInfo[] members = type.GetMembers(flags);
                    foreach (var member in members)
                    {
                        if (member.CustomAttributes.Count() > 0)
                        {
                            ExposedFieldAttribute attribute = member.GetCustomAttribute<ExposedFieldAttribute>();
                            if (attribute != null)
                            {
                                exposedMembers.Add(new ExposedFieldInfo(member, attribute, type));
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Exposed Properties", EditorStyles.boldLabel);
        // BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        foreach (var member in exposedMembers)
        {
            MemberInfo memberInfo = member.memberInfo;
            ExposedFieldAttribute attribute = member.attribute;
            System.Type type = member.type;
            if (memberInfo.MemberType == MemberTypes.Field)
            {
                if (type.BaseType.GetProperty("instance") == null)
                    continue;
                FieldInfo field = (FieldInfo)memberInfo;
                object value = field.GetValue(type.BaseType.GetProperty("instance").GetValue(null));
                foreach (System.Type iType in value.GetType().GetInterfaces())
                {
                    if (iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                    {
                        List<string> logInfo = typeof(EditorParse).GetMethod("ParseDictionary")
                            .MakeGenericMethod(iType.GetGenericArguments())
                            .Invoke(null, new object[] { value }) as List<string>;
                        foreach (var item in logInfo)
                        {
                            EditorGUILayout.LabelField(item);
                        }
                        break;
                    }
                }
            }
            // EditorGUILayout.LabelField();
        }
    }

    public class EditorParse
    {
        public static List<string> ParseDictionary<TKey, TValue>(IDictionary<TKey, TValue> data)
        {
            List<string> eachLog = new List<string>();
            foreach (var pair in data)
            {
                eachLog.Add($"{pair.Key} = {pair.Value}");
            }
            return eachLog;
        }
    }
}