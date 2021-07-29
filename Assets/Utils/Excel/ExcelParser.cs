using UnityEngine;

public class ExcelParser
{
    public static void AutoParse(System.Type inputType, System.Type outputType)
    {
        // inputType.
        Debug.Log("Input Type: " + inputType.ToString());
        Debug.Log("Output Type: " + outputType.ToString());
    }
    
    
    public static int StringToInt(string str)
    {
        return System.Convert.ToInt32(str);
    }
}