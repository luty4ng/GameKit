

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

namespace GameKit
{
    public class ExcelCodeGenerator
    {
        #region --- Create Code ---
        public static Dictionary<string, List<string>> enumDic;
        public static string dataName = "DefaultSO";
        public static string poolName = "DefaultPool";
        private static void Initialize()
        {
            dataName = "DefaultSO";
            poolName = "DefaultPool";
            enumDic = new Dictionary<string, List<string>>();
        }
        // 生成数据代码
        public static string CreateAssetCode(ExcelMediumData excelData)
        {
            Initialize();
            if (excelData == null)
                return null;
            dataName = excelData.excelName + "Data";
            poolName = excelData.excelName + "Pool";
            if (string.IsNullOrEmpty(dataName))
                return null;
            Dictionary<string, string> propertyType = excelData.propertyType;
            if (propertyType == null || propertyType.Count == 0)
                return null;
            if (excelData.dataEachLine == null || excelData.dataEachLine.Count == 0)
                return null;

            StringBuilder classSource = new StringBuilder();
            classSource.Append("/* This is an auto-generated meta script. */\n/* if you want to edit it, please dont use the ScriptToExcel feature, which might cause unhandled error.*/\n");
            classSource.Append("\n");
            classSource.Append("using UnityEngine;\n");
            classSource.Append("using System.Collections.Generic;\n");
            classSource.Append("using System.IO;\n");
            classSource.Append("using UnityEditor;\n");
            classSource.Append("using GameKit;\n");
            classSource.Append("\n");
            classSource.Append(CreateDataClass(dataName, propertyType));
            classSource.Append("\n");
            return classSource.ToString();
        }

        // 生成数据池代码
        public static string CreatePoolCode(ExcelMediumData excelData)
        {
            Initialize();
            if (excelData == null)
                return null;
            dataName = excelData.excelName + "Data";
            poolName = excelData.excelName + "Pool";
            if (string.IsNullOrEmpty(dataName))
                return null;
            Dictionary<string, string> propertyType = excelData.propertyType;
            if (propertyType == null || propertyType.Count == 0)
                return null;
            if (excelData.dataEachLine == null || excelData.dataEachLine.Count == 0)
                return null;
            StringBuilder classSource = new StringBuilder();
            classSource.Append("/* This is an auto-generated meta script. */\n/* if you want to edit it, please dont use the ScriptToExcel feature, which might cause unhandled error.*/\n");
            classSource.Append("\n");
            classSource.Append("using UnityEngine;\n");
            classSource.Append("using System.Collections.Generic;\n");
            classSource.Append("using System.Linq;\n");
            classSource.Append("using System.IO;\n");
            classSource.Append("using UnityEditor;\n");
            classSource.Append("using GameKit;\n");
            classSource.Append("\n");
            classSource.Append(GeneratePoolClass(poolName, dataName));
            classSource.Append("\n");
            classSource.Append(GenerateAssetCreator(excelData));
            classSource.Append("\n");
            return classSource.ToString();
        }

        //数据赋值代码
        private static StringBuilder CreateDataClass(string dataName, Dictionary<string, string> propertyType)
        {
            StringBuilder classSource = new StringBuilder();
            classSource.Append("[System.Serializable]\n");
            classSource.Append("public class " + dataName + " : BaseData\n");
            classSource.Append("{\n");
            classSource.Append("\t#region --- Auto Config --- \n");
            foreach (var item in propertyType)
            {
                classSource.Append(CreateCodeProperty(item.Key, item.Value));
            }
            classSource.Append("\t#endregion \n");
            classSource.Append("}\n");
            return classSource;
        }

        //数据池代码
        private static StringBuilder GeneratePoolClass(string poolName, string dataName)
        {
            StringBuilder classSource = new StringBuilder();
            classSource.Append("[CreateAssetMenu(fileName = \"" + poolName + "\", menuName = \"Excel2SO/Create " + poolName + "\", order = 1)]\n");
            classSource.Append("public class " + poolName + " : BasePool<" + dataName + ">\n");
            classSource.Append("{\n");
            classSource.Append("}\n");
            return classSource;
        }

        //Asset生成代码，通过反射调用
        private static StringBuilder GenerateAssetCreator(ExcelMediumData excelData)
        {
            if (excelData == null)
                return null;
            if (string.IsNullOrEmpty(dataName))
                return null;
            Dictionary<string, string> propertyType = excelData.propertyType;
            if (propertyType == null || propertyType.Count == 0)
                return null;

            List<Dictionary<string, string>> allItemValueRowList = excelData.dataEachLine;
            if (allItemValueRowList == null || allItemValueRowList.Count == 0)
                return null;

            StringBuilder classSource = new StringBuilder();
            classSource.Append("#if UNITY_EDITOR\n");
            //类名
            classSource.Append("public class " + dataName + "AssetAssignment\n");
            classSource.Append("{\n");
            //方法名
            classSource.Append("\tpublic static bool CreateAsset(List<Dictionary<string, string>> dataList, string excelAssetPath)\n");
            //方法体
            classSource.Append("\t{\n");
            classSource.Append("\t\tif (dataList == null || dataList.Count == 0)\n");
            classSource.Append("\t\t\treturn false;\n");
            classSource.Append("\t\tint rowCount = dataList.Count;\n");
            classSource.Append("\t\t" + dataName + "[] items = new " + dataName + "[rowCount];\n");
            classSource.Append("\t\tfor (int i = 0; i < items.Length; i++)\n");
            classSource.Append("\t\t{\n");
            classSource.Append("\t\t\titems[i] = new " + dataName + "();\n");
            foreach (var item in propertyType)
            {
                classSource.Append("\t\t\titems[i]." + item.Key + " = ");
                classSource.Append(AssignmentValue("dataList[i][\"" + item.Key + "\"]", propertyType[item.Key]));
                classSource.Append(";\n");
            }
            classSource.Append("\t\t}\n");
            classSource.Append("\t\t" + poolName + " excelDataAsset = ScriptableObject.CreateInstance<" + poolName + ">();\n");
            classSource.Append("\t\texcelDataAsset.pool = new List<" + dataName + ">(items);\n");
            classSource.Append("\t\tif (!Directory.Exists(excelAssetPath))\n");
            classSource.Append("\t\t\tDirectory.CreateDirectory(excelAssetPath);\n");
            classSource.Append("\t\tstring pullPath = excelAssetPath + \"/\" + typeof(" + poolName + ").Name + \".asset\";\n");
            classSource.Append("\t\tUnityEditor.AssetDatabase.DeleteAsset(pullPath);\n");
            classSource.Append("\t\tUnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);\n");
            classSource.Append("\t\tUnityEditor.AssetDatabase.Refresh();\n");
            classSource.Append("\t\treturn true;\n");
            classSource.Append("\t}\n");
            classSource.Append("}\n");
            classSource.Append("#endif\n");
            return classSource;
        }

        //判断类型转换方法，属于反射调用代码块
        private static string AssignmentValue(string stringValue, string type)
        {
            string resValue = stringValue;
            string generic = "";
            if (type.Split('|').Length == 1)
            {
                generic = "<" + CheckValueType(type).Trim() + ">";
                resValue = "ExcelParser.ParseValueType" + generic + "(" + stringValue + ")";
            }
            else if (type.StartsWith("list") || type.StartsWith("List") || type.StartsWith("LIST"))
            {
                generic = "<" + type.Split('|').LastOrDefault().Trim() + ">";
                resValue = "ExcelParser.ParseList" + generic + "(" + stringValue + ")";
            }
            else if (type.StartsWith("dictionary") || type.StartsWith("Dictionary") || type.StartsWith("DICTIONARY"))
            {
                string pair = type.Split('|').LastOrDefault().Trim();
                string keyStr = "string", valueStr = "string";
                keyStr = CheckValueType(pair.Split(',')[0].Trim());
                valueStr = CheckValueType(pair.Split(',')[1].Trim());
                generic = "<" + keyStr + ", " + valueStr + ">";
                resValue = "ExcelParser.ParseDictionary" + generic + "(" + stringValue + ")";
            }
            return resValue;
        }

        #endregion

        //判断声明类型
        private static string CreateCodeProperty(string name, string type)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            if (name == "idName")
                return null;
            type = type.Trim();
            string generic = "";
            if (type == "int" || type == "Int" || type == "INT")
                type = "int";
            else if (type == "float" || type == "Float" || type == "FLOAT")
                type = "float";
            else if (type == "bool" || type == "Bool" || type == "BOOL")
                type = "bool";
            else if (type.StartsWith("list") || type.StartsWith("List") || type.StartsWith("LIST"))
            {
                generic = "<" + type.Split('|').LastOrDefault().Trim() + ">";
                type = "List";
            }
            else if (type.StartsWith("dictionary") || type.StartsWith("Dictionary") || type.StartsWith("DICTIONARY"))
            {
                string pair = type.Split('|').LastOrDefault().Trim();
                string keyStr = "string", valueStr = "string";
                keyStr = CheckValueType(pair.Split(',')[0].Trim());
                valueStr = CheckValueType(pair.Split(',')[1].Trim());
                generic = "<" + keyStr + ", " + valueStr + ">";
                type = "Dictionary";
            }
            else
                type = "string";

            string propertyStr = "\tpublic " + type + generic + " " + name + ";\n";
            return propertyStr;
        }

        // 用于泛型类型判断
        public static string CheckValueType(string typeName)
        {
            string type = "string";
            if (typeName == "int" || typeName == "Int" || typeName == "INT")
                type = "int";
            else if (typeName == "float" || typeName == "Float" || typeName == "FLOAT")
                type = "float";
            else if (typeName == "bool" || typeName == "Bool" || typeName == "BOOL")
                type = "bool";
            return type;
        }
    }
}

