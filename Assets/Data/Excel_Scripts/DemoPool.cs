/* This is an auto-generated meta script. */
/* if you want to edit it, please dont use the ScriptToExcel feature, which might cause unhandled error.*/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DemoPool", menuName = "Excel2SO/Create DemoPool", order = 1)]
public class DemoPool : BasePool<DemoData>
{
}

#if UNITY_EDITOR
public class DemoDataAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> dataList, string excelAssetPath)
	{
		if (dataList == null || dataList.Count == 0)
			return false;
		int rowCount = dataList.Count;
		DemoData[] items = new DemoData[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new DemoData();
			items[i].id = ExcelParser.ParseValueType<string>(dataList[i]["id"]);
			items[i].showName = ExcelParser.ParseValueType<string>(dataList[i]["showName"]);
			items[i].desc = ExcelParser.ParseValueType<string>(dataList[i]["desc"]);
			items[i].maxOverlap = ExcelParser.ParseValueType<int>(dataList[i]["maxOverlap"]);
			items[i].itemType = ExcelParser.ParseEnum<ItemTypeEnum>(dataList[i]["itemType"]);
			items[i].itemList = ExcelParser.ParseList<string>(dataList[i]["itemList"]);
			items[i].itemDic = ExcelParser.ParseDictionary<string, string>(dataList[i]["itemDic"]);
		}
		DemoPool excelDataAsset = ScriptableObject.CreateInstance<DemoPool>();
		excelDataAsset.pool = new List<DemoData>(items);
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(DemoPool).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


