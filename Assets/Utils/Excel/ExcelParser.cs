using UnityEngine;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using System.Linq;
public class ExcelParser
{
    public static List<T> ParseList<T>(string str, string type)
    {
        List<T> resList = new List<T>();
        string[] strArray = str.Trim().Split(',');
        foreach (var each in strArray)
        {
            resList.Add(ParseValueType<T>(type, each));
        }
        return resList;
    }

    public static Dictionary<T0, T1> ParseDictionary<T0, T1>(string str, string typeA, string typeB)
    {
        Dictionary<T0, T1> resDic = new Dictionary<T0, T1>();
        string[] strArray = str.Trim().Split('/');
        foreach (var each in strArray)
        {
            string keyStr = each.Split(',')[0];
            string valueStr = each.Split(',')[1];
            T0 key = ParseValueType<T0>(keyStr, typeA);
            T1 value = ParseValueType<T1>(valueStr, typeB);
            resDic.Add(key, value);
        }
        return resDic;
    }

    public static T ParseEnum<T>(string str, string property)
    {
        return (T)System.Enum.Parse(typeof(T), property);
    }

    public static T ParseValueType<T>(string type, string value)
    {
        return (T)System.Convert.ChangeType(value, typeof(T));
    }
    public static bool IsValueType(string type)
    {
        if (type.Split('|').Length > 1)
            return true;
        return false;
    }
}