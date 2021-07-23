using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Data;
using UnityEditor;

namespace EditorTool {
    public class ExcelBuild : Editor {
 
        [MenuItem("CustomEditor/CreateItemAsset")]
        public static void CreateItemAsset() {
            ItemModel itemModel = ScriptableObject.CreateInstance<ItemModel>();
            //赋值
            itemModel.dataArray = new List<Item>(ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelsFolderPath + "Item.xlsx"));
 
            //确保文件夹存在
            if(!Directory.Exists(ExcelConfig.scriptableObjectPath)) {
                Directory.CreateDirectory(ExcelConfig.scriptableObjectPath);
            }
 
            //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.scriptableObjectPath, "ItemModel");
            //生成一个Asset文件
            AssetDatabase.CreateAsset(itemModel, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        [MenuItem("CustomEditor/LoadItemAsset")]
        public static void LoadItemAsset() {
            List<Item> dataPool = new List<Item>(ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelsFolderPath + "Item.xlsx"));
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
            //赋值
            itemModel.dataArray = new List<Item>(ExcelTool.CreateItemSOWithExcel(ExcelConfig.excelsFolderPath + "BuildingData.xlsx"));
 
            //确保文件夹存在
            if(!Directory.Exists(ExcelConfig.scriptableObjectPath)) {
                Directory.CreateDirectory(ExcelConfig.scriptableObjectPath);
            }
 
            //asset文件的路径 要以"Assets/..."开始，否则CreateAsset会报错
            string assetPath = string.Format("{0}{1}.asset", ExcelConfig.scriptableObjectPath, "ItemModel");
            //生成一个Asset文件

            AssetDatabase.CreateAsset(itemModel, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
