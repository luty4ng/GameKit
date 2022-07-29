using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameKit
{
    public class ExcelParser
    {
        public static List<T> ParseList<T>(string data)
        {
            List<T> resList = new List<T>();
            string[] strArray = data.Trim().Split(',');
            foreach (var each in strArray)
            {
                resList.Add(ParseValueType<T>(each));
            }
            return resList;
        }

        public static Dictionary<T0, T1> ParseDictionary<T0, T1>(string data)
        {
            Dictionary<T0, T1> resDic = new Dictionary<T0, T1>();
            string[] strArray = data.Trim().Split('|');
            foreach (var each in strArray)
            {
                string keyStr = each.Split(',')[0];
                string valueStr = each.Split(',')[1];
                T0 key = ParseValueType<T0>(keyStr);
                T1 value = ParseValueType<T1>(valueStr);
                resDic.Add(key, value);
            }
            return resDic;
        }

        public static T ParseEnum<T>(string data)
        {
            return (T)System.Enum.Parse(typeof(T), data);
        }

        public static T ParseValueType<T>(string data)
        {
            return (T)System.Convert.ChangeType(data, typeof(T));
        }

    }
}
