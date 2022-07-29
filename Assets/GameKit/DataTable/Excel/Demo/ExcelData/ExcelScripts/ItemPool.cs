/* This is an auto-generated meta script. */
/* if you want to edit it, please dont use the ScriptToExcel feature, which might cause unhandled error.*/

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEditor;
using GameKit;

[CreateAssetMenu(fileName = "ItemPool", menuName = "Excel2SO/Create ItemPool", order = 1)]
public class ItemPool : BasePool<ItemData>
{
}

#if UNITY_EDITOR
public class ItemDataAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> dataList, string excelAssetPath)
	{
		if (dataList == null || dataList.Count == 0)
			return false;
		int rowCount = dataList.Count;
		ItemData[] items = new ItemData[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new ItemData();
			items[i].id = ExcelParser.ParseValueType<string>(dataList[i]["id"]);
			items[i].showName = ExcelParser.ParseValueType<string>(dataList[i]["showName"]);
			items[i].desc = ExcelParser.ParseValueType<string>(dataList[i]["desc"]);
			items[i].maxOverlap = ExcelParser.ParseValueType<int>(dataList[i]["maxOverlap"]);
			items[i].itemType = dataList[i]["itemType"];
			items[i].itemList = ExcelParser.ParseList<string>(dataList[i]["itemList"]);
			items[i].itemDic = ExcelParser.ParseDictionary<string, string>(dataList[i]["itemDic"]);
		}
		ItemPool excelDataAsset = ScriptableObject.CreateInstance<ItemPool>();
		excelDataAsset.pool = new List<ItemData>(items);
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(ItemPool).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


