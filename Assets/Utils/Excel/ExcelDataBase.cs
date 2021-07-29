using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class ExcelDataBase<T> : ScriptableObject where T : ExcelItemBase
{
    public T[] items;
 
    public T GetExcelItem(string targetId)
    {
        if(items != null && items.Length > 0)
        {
            return items.FirstOrDefault(item => item.id == targetId);
        }
        return null;
    }
}
 
public class ExcelItemBase
{
    public string id;
}
