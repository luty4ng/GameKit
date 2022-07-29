using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Excel;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GameKit
{

    public class ExcelMediumData
    {
        public string excelName;
        //Dictionary<字段名称, 字段类型>
        public Dictionary<string, string> propertyType;
        //Dictionary<字段名称, 字段中文提示>
        public Dictionary<string, string> propertyChinese;
        //List<行数据>，List<Dictionary<字段名称, 一行的每个单元格字段值>>
        public List<Dictionary<string, string>> dataEachLine;
    }

    public class ExcelTool
    {

        public static void CreateCode(string excelFileFullPath)
        {
            ExcelMediumData excelMediumData = GenerateExcelMediumData(excelFileFullPath);
            if (excelMediumData != null)
            {
                string dataCodeStr = ExcelCodeGenerator.CreateAssetCode(excelMediumData);
                string poolCodeStr = ExcelCodeGenerator.CreatePoolCode(excelMediumData);
                if (!string.IsNullOrEmpty(dataCodeStr) && !string.IsNullOrEmpty(poolCodeStr))
                {
                    if (WriteCodeStrToSave(ExcelConfig.excelCodePath, excelMediumData.excelName + "Data", dataCodeStr) &&
                        WriteCodeStrToSave(ExcelConfig.excelCodePath, excelMediumData.excelName + "Pool", poolCodeStr))
                    {
                        Utility.Debugger.Log("<color=green>Auto Create Excel Scripts Success : </color>" + excelMediumData.excelName);
                        return;
                    }
                }
            }
            Utility.Debugger.LogError("Auto Create Excel Scripts Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
        }

        public static void CreateAsset(string excelFileFullPath)
        {
            //解析Excel获取中间数据
            ExcelMediumData excelMediumData = GenerateExcelMediumData(excelFileFullPath);
            if (excelMediumData != null)
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                // 创建类的实例，返回为 object 类型，需要强制类型转换，assembly.CreateInstance("类的完全限定名（即包括命名空间）");
                object class0bj = assembly.CreateInstance(excelMediumData.excelName + "Assignment", true);

                //必须遍历所有程序集来获得类型。当前在Assembly-CSharp-Editor中，目标类型在Assembly-CSharp中，不同程序将无法获取类型
                System.Type type = null;
                foreach (var asm in System.AppDomain.CurrentDomain.GetAssemblies())
                {
                    //查找目标类型
                    System.Type tempType = asm.GetType(excelMediumData.excelName + "Data" + "AssetAssignment");
                    if (tempType != null)
                    {
                        type = tempType;
                        break;
                    }
                }
                // Utility.Debugger.Log(type);
                if (type != null)
                {
                    //反射获取方法
                    MethodInfo methodInfo = type.GetMethod("CreateAsset");

                    if (methodInfo != null)
                    {
                        methodInfo.Invoke(null, new object[] { excelMediumData.dataEachLine, ExcelConfig.excelPoolPath });
                        Utility.Debugger.Log("<color=green>Auto Create Excel Asset Success : </color>" + excelMediumData.excelName);
                        return;
                    }
                }
            }
            Utility.Debugger.LogError("Auto Create Excel Asset Fail : " + (excelMediumData == null ? "" : excelMediumData.excelName));
        }
        public static void CreateCodeAll()
        {
            string[] excelFileFullPaths = Directory.GetFiles(Utility.IO.AdaptivePath(ExcelConfig.excelPath), "*.xlsx");
            if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
            {
                Utility.Debugger.Log("Excel file count == 0");
                return;
            }
            for (int i = 0; i < excelFileFullPaths.Length; i++)
            {
                CreateCode(excelFileFullPaths[i]);
            }
        }
        public static void CreateAssetAll()
        {
            string[] excelFileFullPaths = Directory.GetFiles(Utility.IO.AdaptivePath(ExcelConfig.excelPath), "*.xlsx");
            if (excelFileFullPaths == null || excelFileFullPaths.Length == 0)
            {
                Utility.Debugger.Log("Excel file count == 0");
                return;
            }
            for (int i = 0; i < excelFileFullPaths.Length; i++)
            {
                CreateAsset(excelFileFullPaths[i]);
            }
        }

        private static bool WriteCodeStrToSave(string writeFilePath, string codeFileName, string classCodeStr)
        {
            if (string.IsNullOrEmpty(codeFileName) || string.IsNullOrEmpty(classCodeStr))
                return false;
            //检查导出路径
            if (!Directory.Exists(writeFilePath))
                Directory.CreateDirectory(writeFilePath);
            //写文件，生成CS类文件
            StreamWriter sw = new StreamWriter(writeFilePath + "/" + codeFileName + ".cs");
            sw.WriteLine(classCodeStr);
            sw.Close();
            //
#if UNITY_EDITOR
            AssetDatabase.Refresh();
#endif

            return true;
        }

        private static ExcelMediumData GenerateExcelMediumData(string excelFileFullPath)
        {
            if (string.IsNullOrEmpty(excelFileFullPath))
                return null;

            excelFileFullPath = excelFileFullPath.Replace("\\", "/");

            FileStream stream = File.Open(excelFileFullPath, FileMode.Open, FileAccess.Read);
            if (stream == null)
                return null;
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            if (excelReader == null || !excelReader.IsValid)
            {
                Utility.Debugger.Log("Invalid excel ： " + excelFileFullPath);
                return null;
            }

            //<数据名称,数据类型>
            KeyValuePair<string, string>[] propertyNameTypes = null;
            //List<KeyValuePair<数据名称, 单元格数据值>[]>，所有数据值，按行记录
            List<Dictionary<string, string>> allItemValueRowList = new List<Dictionary<string, string>>();

            //每行数据数量
            int propertyCount = 0;
            //当前遍历行，从1开始
            int curRowIndex = 1;
            //开始读取，按行遍历
            while (excelReader.Read())
            {
                if (excelReader.FieldCount == 0)
                    continue;
                //读取一行的数据
                string[] datas = new string[excelReader.FieldCount];
                for (int j = 0; j < excelReader.FieldCount; ++j)
                {
                    //赋值一行的每一个单元格数据
                    datas[j] = excelReader.GetString(j);
                }
                //空行/行第一个单元格为空，视为无效数据
                if (datas.Length == 0 || string.IsNullOrEmpty(datas[0]))
                {
                    curRowIndex++;
                    continue;
                }
                //数据行
                if (curRowIndex >= ExcelConfig.excelDataRow)
                {
                    //数据无效
                    if (propertyCount <= 0)
                        return null;

                    Dictionary<string, string> itemDic = new Dictionary<string, string>(propertyCount);
                    //遍历一行里的每个单元格数据
                    for (int j = 0; j < propertyCount; j++)
                    {
                        //判断长度
                        if (j < datas.Length)
                            itemDic[propertyNameTypes[j].Key] = datas[j];
                        else
                            itemDic[propertyNameTypes[j].Key] = null;
                    }
                    allItemValueRowList.Add(itemDic);
                }
                //数据名称行
                else if (curRowIndex == ExcelConfig.excelNameRow)
                {
                    //以数据名称确定每行的数据数量
                    propertyCount = datas.Length;
                    if (propertyCount <= 0)
                        return null;
                    //记录数据名称
                    propertyNameTypes = new KeyValuePair<string, string>[propertyCount];
                    for (int i = 0; i < propertyCount; i++)
                    {
                        propertyNameTypes[i] = new KeyValuePair<string, string>(datas[i], null);
                    }
                }
                //数据类型行
                else if (curRowIndex == ExcelConfig.excelTypeRow)
                {
                    //数据类型数量少于指定数量，数据无效
                    if (propertyCount <= 0 || datas.Length < propertyCount)
                        return null;
                    //记录数据名称及类型
                    for (int i = 0; i < propertyCount; i++)
                    {
                        propertyNameTypes[i] = new KeyValuePair<string, string>(propertyNameTypes[i].Key, datas[i]);
                    }
                }
                curRowIndex++;
            }

            if (propertyNameTypes.Length == 0 || allItemValueRowList.Count == 0)
                return null;

            ExcelMediumData excelMediumData = new ExcelMediumData();
            //类名
            // excelMediumData.excelName = excelReader.Name;
            excelMediumData.excelName = excelFileFullPath.Split('/').LastOrDefault().Split('.')[0];
            Utility.Debugger.Log(excelMediumData.excelName);
            //Dictionary<数据名称,数据类型>
            excelMediumData.propertyType = new Dictionary<string, string>();
            //转换存储格式
            for (int i = 0; i < propertyCount; i++)
            {
                //数据名重复，数据无效
                if (excelMediumData.propertyType.ContainsKey(propertyNameTypes[i].Key))
                    return null;
                excelMediumData.propertyType.Add(propertyNameTypes[i].Key, propertyNameTypes[i].Value);
            }
            excelMediumData.dataEachLine = allItemValueRowList;
            return excelMediumData;
        }

        #region --- Excel Operation API ---

        private static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }

        private static System.Data.DataTable ReadExcelTable(string filePath, int sheetNum = 0)
        {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            return result.Tables[sheetNum];
        }

        private static void TryGetDataTypes(DataRowCollection rowCollection, out List<string> types)
        {
            types = new List<string>();
            for (int i = 0; i < rowCollection.Count - 1; i++)
            {
                types.Add(rowCollection[1][i].ToString());
            }
        }

        #endregion

        // public static Item[] CreateItemSOWithExcel(string filePath)
        // {

        //     // DataRowCollection collect = ReadExcelByRow(filePath, ref columnNum, ref rowNum);
        //     DataTable table = ReadExcelTable(filePath, 0);
        //     int columnNum = table.Columns.Count, rowNum = table.Rows.Count;
        //     TryGetDataTypes(table.Rows, out List<string> dataType);
        //     Item[] Item = new Item[rowNum - 1];
        //     return Item;
        // }
    }
}
