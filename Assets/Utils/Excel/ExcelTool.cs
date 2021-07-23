using UnityEngine;
using System.Data;
using Data;
using System.IO;
using Excel;

namespace EditorTool {
    public class ExcelTool {
 
        /// <summary>
        /// 读取表数据，生成对应的数组
        /// </summary>
        /// <param name="filePath">excel文件全路径</param>
        /// <returns>Item数组</returns>
        public static Item[] CreateItemArrayWithExcel(string filePath) {
            //获得表数据
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);
 
            //根据excel的定义，第二行开始才是数据
            Item[] array = new Item[rowNum - 1];
            for(int i = 1; i < rowNum; i++) {
                Item item = new Item();
                //解析每列的数据
                item.id =  collect[i][0].ToString();
                item.desc = collect[i][1].ToString();
                item.maxOverlap = int.Parse(collect[i][3].ToString());
                array[i - 1] = item;
            }
            return array;
        }

        public static Item[] CreateItemSOWithExcel(string filePath) {
            //获得表数据
            int columnNum = 0, rowNum = 0;
            DataRowCollection collect = ReadExcel(filePath, ref columnNum, ref rowNum);
 
            //根据excel的定义，第二行开始才是数据
            Item[] Item = new Item[rowNum - 1];
            for(int i = 1; i < rowNum; i++) {
                Item item = new Item();
                Debug.Log(collect[i][1].GetType());
                // item.name = collect[i][0].ToString();
                // item.itemName = collect[i][0].ToString();
                // item.showName = collect[i][0].ToString();
                // item.type = ItemPool.Instance.GetType(collect[i][0].ToString());
                // item.itemDesc = collect[i][0].ToString();
                // item.image = collect[i][0].
                // item.holdImage = collect[i][0].
                // item.maxOverlap = collect[i][0].
                // item.useProjectile = collect[i][0].
                // item.projectileSpeed = collect[i][0].
                // item.coolDown = collect[i][0].
                // item.instanceName = collect[i][0].
                // item.currentOverlap = collect[i][0].
                // item.indexOnInventory = collect[i][0].
                // item.durability = collect[i][0].
                // item.craftFrom = collect[i][0].
                // item.canCraft = collect[i][0].
                // item.costData = collect[i][0].
                // item.costNum = collect[i][0].
                // item.craftTime = collect[i][0].
                Item[i - 1] = item;
            }
            return Item;
        }
 
        /// <summary>
        /// 逐行读取excel文件内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnNum">行数</param>
        /// <param name="rowNum">列数</param>
        /// <returns></returns>
        static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum) {
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            columnNum = result.Tables[0].Columns.Count;
            rowNum = result.Tables[0].Rows.Count;
            return result.Tables[0].Rows;
        }

        public void ParseList()
        {

        }

    }
}
