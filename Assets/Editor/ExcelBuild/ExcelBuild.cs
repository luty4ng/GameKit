using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Data;
using UnityEditor;

namespace EditorTool {
    public class ExcelBuild : Editor {
 
        [MenuItem("CustomEditor/CreateItemAsset")]
        public static void CreateItemAsset() {
            ItemModel itemModel = ScriptableObject.CreateInstance<ItemModel>();
            itemModel.pool = new List<Item>(ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelPath + "Item.xlsx"));

            if(!Directory.Exists(ExcelConfig.excelPoolPath)) {
                Directory.CreateDirectory(ExcelConfig.excelPoolPath);
            }
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.excelPoolPath, "ItemModel");

            AssetDatabase.CreateAsset(itemModel, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomEditor/LoadItemAsset")]
        public static void LoadItemAsset() {
            List<Item> dataPool = new List<Item>(ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelPath + "Item.xlsx"));
            // ItemPool.Instance.pool.Clear();
            // ItemPool.Instance.pool = dataPool;
            //确保文件夹存在
            // if(!Directory.Exists(ExcelConfig.scriptableObjectPath)) {
            //     Directory.CreateDirectory(ExcelConfig.scriptableObjectPath);
            // }
 
            //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
            // string assetPath = string.Format("{0}{1}.asset", ExcelConfig.scriptableObjectPath, "ItemModel");
            //生成一个Asset文件
            // AssetDatabase.CreateAsset(itemModel, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomEditor/LoadBuildingAsset")]
        public static void LoadBuildingAsset() {
            ItemModel itemModel = ScriptableObject.CreateInstance<ItemModel>();
            itemModel.pool = new List<Item>(ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelPath + "BuildingData.xlsx"));
 
            //确保文件夹存在
            if(!Directory.Exists(ExcelConfig.excelPoolPath)) {
                Directory.CreateDirectory(ExcelConfig.excelPoolPath);
            }
 
            //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.excelPoolPath, "ItemModel");
            //生成一个Asset文件

            AssetDatabase.CreateAsset(itemModel, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomEditor/Debug")]
        public static void Debug() {
            // ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelPath + "ItemData.xlsx");
            ExcelTool.ReadAllExcelToCode();
        }

        
    }
}
