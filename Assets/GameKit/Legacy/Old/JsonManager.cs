using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using GameKit;
namespace GameKit
{
    public delegate void JsonAction();
    public interface IJsonTask { }

    public class JsonManager : SingletonBase<JsonManager>
    {
        public List<JsonTasks> jsonTasks;

        public class JsonTasks : IJsonTask
        {
            public JsonAction actions;
            public JsonTasks(JsonAction action)
            {
                actions = action;
            }
        }

        public JsonManager()
        {
            jsonTasks = new List<JsonTasks>();

            if (!Directory.Exists(Application.streamingAssetsPath + "/"))
                Directory.CreateDirectory(Application.streamingAssetsPath + "/");

            MonoManager.instance.AddUpdateListener(JsonUpdate);
        }

        public void LoadJsonDataOverwrite<T>(string fileName, T loadObj)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (!File.Exists(filePath))
                return;
            string dataAsJson = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(dataAsJson, loadObj);
        }

        public void LoadAllJsonOverwrite<T>(string path, T[] loadObj)
        {
            CheckDirExsit(path);
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
            string[] allFile = Directory.GetFiles(filePath);
            int i = 0;
            foreach (var fileName in allFile)
            {
                if (!File.Exists(fileName))
                    continue;
                string dataAsJson = File.ReadAllText(fileName);
                Debug.Log(loadObj[i]);
                JsonUtility.FromJsonOverwrite(dataAsJson, loadObj[i]);
                i++;
            }
        }

        public T LoadJsonData<T>(string fileName)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (!File.Exists(filePath))
                return default(T);
            string dataAsJson = File.ReadAllText(filePath);
            Debug.Log(fileName);
            T loadData = JsonUtility.FromJson<T>(dataAsJson);
            return loadData;
        }

        public T[] LoadAllJson<T>(string path)
        {
            CheckDirExsit(path);
            string filePath = Path.Combine(Application.streamingAssetsPath, path);
            string[] allFile = Directory.GetFiles(filePath);
            T[] allData = new T[allFile.Length];
            int index = 0;
            foreach (var fileName in allFile)
            {
                if (!File.Exists(fileName))
                    continue;
                string dataAsJson = File.ReadAllText(fileName);
                T loadData = JsonUtility.FromJson<T>(dataAsJson);
                allData[index] = loadData;
            }
            return allData;
        }

        public Dictionary<Key, Value> LoadJsonDict<Key, Value>(string fileName)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (!File.Exists(filePath))
                return new Dictionary<Key, Value>();
            string dataAsJson = File.ReadAllText(filePath);

            Dictionary<Key, Value> loadData = JsonUtility.FromJson<SerializedDictionary<Key, Value>>(dataAsJson).ToDictionary();
            return loadData;
        }

        public List<T> LoadJsonList<T>(string fileName)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (!File.Exists(filePath))
                return new List<T>();
            string dataAsJson = File.ReadAllText(filePath);

            List<T> loadData = JsonUtility.FromJson<Serialization<T>>(dataAsJson).ToList(); ;
            return loadData;
        }

        public void SaveJsonData<T>(string fileName, T data)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (File.Exists(filePath))
                File.Delete(filePath);
            File.Create(filePath).Dispose();
            string jsonData = JsonUtility.ToJson(data);
            File.WriteAllText(filePath, jsonData);
        }

        public void SaveJsonDict<Key, Value>(string fileName, Dictionary<Key, Value> dicData)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (File.Exists(filePath))
                File.Delete(filePath);
            File.Create(filePath).Dispose();

            string jsonData = JsonUtility.ToJson(new SerializedDictionary<Key, Value>(dicData));
            File.WriteAllText(filePath, jsonData);
        }

        public void SaveJsonList<T>(string fileName, List<T> listData)
        {
            CheckDirExsit(fileName);
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            if (File.Exists(filePath))
                File.Delete(filePath);
            File.Create(filePath).Dispose();

            string jsonData = JsonUtility.ToJson(new Serialization<T>(listData));
            File.WriteAllText(filePath, jsonData);
        }

        private void CheckDirExsit(string path)
        {
            if (path.Contains("/"))
            {
                string[] eachDir = path.Split('/');
                string dir = Application.streamingAssetsPath;
                for (int i = 0; i < eachDir.Length - 1; i++)
                {
                    if (!Directory.Exists(dir + "/" + eachDir[i] + "/"))
                    {
                        Directory.CreateDirectory(dir + "/" + eachDir[i] + "/");
                    }
                }
            }
            else
            {
                string dir = Application.streamingAssetsPath;
                if (!Directory.Exists(dir + "/" + path + "/"))
                {
                    Directory.CreateDirectory(dir + "/" + path + "/");
                }

            }
        }

        public bool CheckJsonExist(string fileName)
        {
            bool res = false;
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
            res = File.Exists(filePath);
            return res;
        }

        public void AddJsonTask<T>(bool isRead, string fileName, T data)
        {

            if (isRead)
            {
                jsonTasks.Add(new JsonTasks(() =>
                {
                    SaveJsonData<T>(fileName, data);
                }));
            }
            else
            {
                jsonTasks.Add(new JsonTasks(() =>
                {
                    LoadJsonData<T>(fileName);
                }));
            }
        }

        public void JsonUpdate()
        {
            if (jsonTasks.Count == 0)
                return;
            jsonTasks[0].actions.Invoke();
        }

        private void Close()
        {

        }
    }
}
