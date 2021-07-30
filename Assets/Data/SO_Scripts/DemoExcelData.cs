/* This is an auto generated meta-script, don't Edit it.*/

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;

[Serializable]
public class DemoExcelItem : ExcelItemBase
{
	public string showName;
	public string desc;
	public int maxOverlap;
}

[CreateAssetMenu(fileName = "DemoExcelData", menuName = "Excel2SO/Create DemoExcelData", order = 1)]
public class DemoExcelData : ExcelDataBase<DemoExcelItem>
{
}

#if UNITY_EDITOR
public class DemoAssetAssignment
{
	public static bool CreateAsset(List<Dictionary<string, string>> allItemValueRowList, string excelAssetPath)
	{
		if (allItemValueRowList == null || allItemValueRowList.Count == 0)
			return false;
		int rowCount = allItemValueRowList.Count;
		DemoExcelItem[] items = new DemoExcelItem[rowCount];
		for (int i = 0; i < items.Length; i++)
		{
			items[i] = new DemoExcelItem();
			items[i].id = allItemValueRowList[i]["id"];
			items[i].showName = allItemValueRowList[i]["showName"];
			items[i].desc = allItemValueRowList[i]["desc"];
			items[i].maxOverlap = Convert.ToInt32(allItemValueRowList[i]["maxOverlap"]);
			// items[i].itemType = (TheType)(Convert.ToInt32(allItemValueRowList[i]["itemType"]));
		}
		DemoExcelData excelDataAsset = ScriptableObject.CreateInstance<DemoExcelData>();
		excelDataAsset.items = items;
		if (!Directory.Exists(excelAssetPath))
			Directory.CreateDirectory(excelAssetPath);
		string pullPath = excelAssetPath + "/" + typeof(DemoExcelData).Name + ".asset";
		UnityEditor.AssetDatabase.DeleteAsset(pullPath);
		UnityEditor.AssetDatabase.CreateAsset(excelDataAsset, pullPath);
		UnityEditor.AssetDatabase.Refresh();
		return true;
	}
}
#endif


