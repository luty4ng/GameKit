using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Excel;
using System.IO;

namespace Data
{
    public class ItemModel : ScriptableObject
    {
        public List<Item> dataArray;
        // FileStream stream = File.Open("filePath", FileMode.Open, FileAccess.Read, FileShare.Read);
    }

}