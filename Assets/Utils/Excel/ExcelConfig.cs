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
        //Excel第x行对应字段类型
        public const int excelTypeRow = 3;
        //Excel第x行及以后对应字段值
        public const int excelDataRow = 4;
        // 存放Excel表
        public static readonly string excelPath = "Assets/Data/Excels/";

        // 存放自动生成cs模板
        public static readonly string excelCodePath = "Assets/Data/SO_Scripts/";

        // 存放Excel生成的SO池
        public static readonly string excelPoolSOPath = "Assets/Data/SO_Assets/Pool/";

        // 存放Excel生成的独立SO
        public static readonly string excelIndieSOPath = "Assets/Data/SO_Assets/Individual/";

    }
}
