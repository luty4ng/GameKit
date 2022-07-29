using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections;
using UnityEngine;
namespace GameKit
{
    public static partial class Utility
    {
        public static class Debugger
        {
            [ThreadStatic]
            private static StringBuilder s_CachedStringBuilder = null;
            public static void Log(string info) => Debug.Log(info);
            public static void LogWarning(string info) => Debug.Log(Utility.Text.Format("<b><color=orange>[Warning]</color></b> {0}", info));
            public static void LogError(string info) => Debug.LogError(Utility.Text.Format("<b><color=red>[Error]</color></b> {0}", info));
            public static void LogSuccess(string info) => Debug.Log(Utility.Text.Format("<b><color=green>[Success]</color></b> {0}", info));
            public static void LogFail(string info) => Debug.Log(Utility.Text.Format("<b><color=red>[Failed]</color></b> {0}", info));
            public static void LogExcute(string info, bool success)
            {
                if (success)
                    LogSuccess(info);
                else
                    LogFail(info);
            }

            public static void LogWarning(string info, object arg0)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0);
                LogWarning(s_CachedStringBuilder.ToString());
            }

            public static void LogWarning(string info, object arg0, object arg1)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1);
                LogWarning(s_CachedStringBuilder.ToString());
            }

            public static void LogWarning(string info, object arg0, object arg1, object arg2)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1, arg2);
                LogWarning(s_CachedStringBuilder.ToString());
            }

            public static void LogError(string info, object arg0)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0);
                LogError(s_CachedStringBuilder.ToString());
            }

            public static void LogError(string info, object arg0, object arg1)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1);
                LogError(s_CachedStringBuilder.ToString());
            }

            public static void LogError(string info, object arg0, object arg1, object arg2)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1, arg2);
                LogError(s_CachedStringBuilder.ToString());
            }

            public static void Log(string info, object arg0)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0);
                Log(s_CachedStringBuilder.ToString());
            }

            public static void Log(string info, object arg0, object arg1)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1);
                Log(s_CachedStringBuilder.ToString());
            }

            public static void Log(string info, object arg0, object arg1, object arg2)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1, arg2);
                Log(s_CachedStringBuilder.ToString());
            }

            public static void LogSuccess(string info, object arg0)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0);
                LogSuccess(s_CachedStringBuilder.ToString());
            }

            public static void LogSuccess(string info, object arg0, object arg1)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1);
                LogSuccess(s_CachedStringBuilder.ToString());
            }

            public static void LogSuccess(string info, object arg0, object arg1, object arg2)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1, arg2);
                LogSuccess(s_CachedStringBuilder.ToString());
            }

            public static void LogFail(string info, object arg0)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0);
                LogFail(s_CachedStringBuilder.ToString());
            }

            public static void LogFail(string info, object arg0, object arg1)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1);
                LogFail(s_CachedStringBuilder.ToString());
            }

            public static void LogFail(string info, object arg0, object arg1, object arg2)
            {
                CheckCachedStringBuilder();
                s_CachedStringBuilder.Length = 0;
                s_CachedStringBuilder.AppendFormat(info, arg0, arg1, arg2);
                LogFail(s_CachedStringBuilder.ToString());
            }

            private static void CheckCachedStringBuilder()
            {
                if (s_CachedStringBuilder == null)
                {
                    s_CachedStringBuilder = new StringBuilder(1024);
                }
            }
        }
    }
}

