using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace GameKit
{
    public static partial class Utility
    {
        private static Boolean LogicEx(Boolean a1, Boolean a2, string re)
        {
            Boolean result = false;
            switch (re)
            {
                case "&&": result = a1 && a2; break;
                case "||": result = a1 || a2; break;
            }
            return result;
        }



        // public Boolean LogicExpression(string str)
        // {
        //     Boolean result = false;
        //     DataTable dt = new DataTable();
        //     string[] logicExData = Regex.Split(str, @"\|\||&&", RegexOptions.IgnoreCase);
        //     string logicStr = "||";
        //     for (int logicExIndex = 0; logicExIndex < logicExData.Length; logicExIndex++)
        //     {
        //         if (logicExIndex != 0)
        //         {
        //             logicStr = str.Substring(str.IndexOf(logicExData[logicExIndex - 1]) + logicExData[logicExIndex - 1].Length, 2);
        //         }
        //         Boolean re = (Boolean)dt.Compute(logicExData[logicExIndex], "");
        //         result = LogicEx(result, re, logicStr);
        //     }
        //     return result;
        // }
    }
}

