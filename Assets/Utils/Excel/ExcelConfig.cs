using UnityEngine;

namespace EditorTool {
 
    public class ExcelConfig {
        /// <summary>
        /// 存放excel表文件夹的的路径，本例xecel表放在了"Assets/Excels/"当中
        /// </summary>
        public static readonly string excelsFolderPath = Application.dataPath + "/Data/Excels/";
 
        /// <summary>
        /// 存放Excel转化CS文件的文件夹路径
        /// </summary>
        public static readonly string assetPath = "Assets/Resources/DataAssets/";

        /// <summary>
        /// 存放Excel生成的SO池
        /// </summary>
        public static readonly string scriptableObjectPath = "Assets/Data/ScriptableObjects/Resources/";
        
    }
}
