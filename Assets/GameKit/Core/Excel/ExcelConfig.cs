using UnityEngine;

namespace EditorTool
{
    /// <summary>
    /// 注: 起始相对路径为项目根目录
    /// </summary>
    public class ExcelConfig
    {
        //Excel第x行对应字段名称
        public const int excelNameRow = 1;
        //Excel第x行对应字段C#属性名称（暂时未解析）
        // public const int excelAttributeRow = 2;
        //Excel第x行对应字段类型
        public const int excelTypeRow = 3;
        //Excel第x行及以后对应字段值
        public const int excelDataRow = 4;
        // 存放Excel表
        public static readonly string excelPath = "Assets/GameKit/Core/Excel/ExcelData/Excels/";
        // 存放自动生成cs模板
        public static readonly string excelCodePath = "Assets/GameKit/Core/Excel/ExcelData/Excel_Scripts/";
        // 存放Excel生成的Asset池
        public static readonly string excelPoolPath = "Assets/GameKit/Core/Excel/ExcelData/Excel_Assets/";
        // 存放Excel生成的独立Asset（如需）
        public static readonly string excelAssetPath = "Assets/GameKit/Core/Excel/ExcelData/Excel_Assets/";

    }
}
