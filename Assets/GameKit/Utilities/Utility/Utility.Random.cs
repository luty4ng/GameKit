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
        public static class Random
        {
            private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            private static System.Random s_Random = new System.Random((int)DateTime.UtcNow.Ticks);
            public static string EncryptWithMD5(string source)
            {
                byte[] sor = Encoding.UTF8.GetBytes(source);
                MD5 md5 = MD5.Create();
                byte[] result = md5.ComputeHash(sor);
                StringBuilder strbul = new StringBuilder();
                for (int i = 0; i < result.Length; i++)
                {
                    strbul.Append(result[i].ToString("x2"));

                }
                return strbul.ToString();
            }

            public static string GetRandomString(int length, bool useNum = false, bool useSym = false)
            {
                byte[] bytes = new byte[4];
                rngCsp.GetBytes(bytes);
                System.Random random = new System.Random(BitConverter.ToInt32(bytes, 0));
                string str = null, strSets = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
                if (useNum == true)
                {
                    strSets += "0123456789";
                }
                if (useSym == true)
                {
                    strSets += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
                }
                for (int i = 0; i < length; i++)
                {
                    str += strSets.Substring(random.Next(0, strSets.Length - 1), 1);
                }
                return str;
            }

            public static string GetRandomID()
            {
                return GetRandomString(8, useNum: true);
            }

            public static void SetSeed(int seed)
            {
                s_Random = new System.Random(seed);
            }

            public static int GetRandom()
            {
                return s_Random.Next();
            }

            public static int GetRandom(int maxValue)
            {
                return s_Random.Next(maxValue);
            }

            public static int GetRandom(int minValue, int maxValue)
            {
                return s_Random.Next(minValue, maxValue);
            }

            public static double GetRandomDouble()
            {
                return s_Random.NextDouble();
            }

            public static void GetRandomBytes(byte[] buffer)
            {
                s_Random.NextBytes(buffer);
            }
        }
    }
}