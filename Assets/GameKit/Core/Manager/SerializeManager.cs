using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using LitJson;

/// <summary>
/// 非异步文件加载管理器
/// </summary>

namespace GameKit
{
    public delegate void FileStreamAction<FileStream>(FileStream fileStream);
    public class SerializeManager : SingletonBase<SerializeManager>
    {
        public static string TARGET_PATH = Application.streamingAssetsPath;
        private static BinaryFormatter binaryFormatter;
        public SerializeManager()
        {
            binaryFormatter = new BinaryFormatter();
            if (!Directory.Exists(Utility.Text.Format(TARGET_PATH, "/")))
                Directory.CreateDirectory(Utility.Text.Format(TARGET_PATH, "/"));
        }

        public T[] LoadJsonFromDirectory<T>(string fileName) where T : class
        {
            string filePath = Path.Combine(TARGET_PATH, fileName);
            PathFix(filePath);
            string[] allFile = Directory.GetFiles(filePath);
            T[] allData = new T[allFile.Length];
            for (int i = 0; i < allFile.Length; i++)
            {
                allData[i] = LoadJson<T>(fileName);
            }
            return allData;
        }

        public void SaveJson<T>(string fileName, T data) where T : class
        {
            InvokeDisposableFileAction(fileName, FileMode.Open, (FileStream stream) =>
            {
                string jsonData = JsonMapper.ToJson(data);
                Debug.Log(jsonData);
                byte[] byteData = Encoding.Default.GetBytes(jsonData);
                stream.Write(byteData, 0, byteData.Length);
            });
        }

        public void SaveJson<T>(string fileName, T data, Encoding encoding) where T : class
        {
            InvokeDisposableFileAction(fileName, FileMode.Open, (FileStream stream) =>
            {
                string jsonData = JsonMapper.ToJson(data);
                byte[] byteData = encoding.GetBytes(jsonData);
                stream.Write(byteData, 0, byteData.Length);
            });
        }

        public T LoadJson<T>(string fileName) where T : class
        {
            T result = default(T);
            InvokeDisposableFileAction(fileName, FileMode.Open, (FileStream stream) =>
            {
                byte[] byteData = new byte[(int)stream.Length];
                stream.Read(byteData, 0, byteData.Length);
                string data = Encoding.Default.GetString(byteData);
                T loadData = JsonMapper.ToObject<T>(data);
            });
            return result;
        }

        public T LoadJson<T>(string fileName, Encoding encoding) where T : class
        {
            T result = default(T);
            InvokeDisposableFileAction(fileName, FileMode.Open, (FileStream stream) =>
            {
                byte[] byteData = new byte[(int)stream.Length];
                stream.Read(byteData, 0, byteData.Length);
                string data = encoding.GetString(byteData);
                T loadData = JsonMapper.ToObject<T>(data);
            });
            return result;
        }

        public void SaveBinary<T>(string fileName, T data) where T : class
        {
            InvokeDisposableFileAction(fileName, FileMode.Create, (FileStream stream) =>
            {
                binaryFormatter.Serialize(stream, data);
            });
        }

        public T LoadBinary<T>(string fileName) where T : class
        {
            T result = default(T);
            InvokeDisposableFileAction(fileName, FileMode.Create, (FileStream stream) =>
            {
                result = binaryFormatter.Deserialize(stream) as T;
            });
            return result;
        }

        public T[] LoadBinaryFromDirectory<T>(string fileName) where T : class
        {
            string filePath = Path.Combine(TARGET_PATH, fileName);
            PathFix(filePath);
            string[] allFile = Directory.GetFiles(filePath);
            T[] allData = new T[allFile.Length];
            for (int i = 0; i < allFile.Length; i++)
            {
                allData[i] = LoadBinary<T>(fileName);
            }
            return allData;
        }

        public void UseStreamingPath() => TARGET_PATH = Application.streamingAssetsPath;
        public void UsePersistentPath() => TARGET_PATH = Application.persistentDataPath;
        public void UseDataPath() => TARGET_PATH = Application.dataPath;

        #region private
        private void CheckDirExsit(string path, string filePath)
        {
            string[] eachDir = filePath.Split('/');
            for (int i = 0; i < eachDir.Length - 1; i++)
            {
                if (!Directory.Exists(path + "/" + eachDir[i] + "/"))
                {
                    Directory.CreateDirectory(path + "/" + eachDir[i] + "/");
                }
            }
        }

        private FileStream GetFileStream(string filePath, string fileName, FileMode fileMode)
        {
            CheckDirExsit(filePath, fileName);
            string path = Path.Combine(filePath, fileName);
            FileStream stream;
            if (!File.Exists(path))
                stream = File.Create(path);
            else
                stream = new FileStream(path, fileMode);
            return stream;
        }

        private FileStream GetFileStream(string fileName, FileMode fileMode)
        {
            return GetFileStream(TARGET_PATH, fileName, fileMode);
        }

        private void InvokeDisposableFileAction(string fileName, FileMode fileMode, FileStreamAction<FileStream> action = null)
        {
            FileStream fileStream = GetFileStream(TARGET_PATH, fileName, fileMode);
            action?.Invoke(fileStream);
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
        }
        private void PathFix(string fileName) => CheckDirExsit(TARGET_PATH, fileName);

        #endregion
    }
}
