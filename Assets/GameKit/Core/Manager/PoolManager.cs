using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;
namespace GameKit
{
    public class PoolData
    {
        public GameObject fatherObj;
        public List<GameObject> poolList;

        public PoolData(GameObject obj, GameObject poolObj)
        {
            fatherObj = new GameObject(obj.name);
            fatherObj.transform.SetParent(poolObj.transform);
            poolList = new List<GameObject>() { };
            PushObj(obj);
        }

        public void PushObj(GameObject gameobj)
        {
            poolList.Add(gameobj);
            gameobj.transform.SetParent(fatherObj.transform);
            gameobj.SetActive(false);
        }

        public GameObject GetObj()
        {
            GameObject gameobj = null;
            gameobj = poolList[0];
            poolList.RemoveAt(0);
            gameobj.transform.SetParent(null);
            gameobj.SetActive(true);
            return gameobj;
        }
    }

    public class PoolManager : SingletonBase<PoolManager>
    {
        private Dictionary<string, PoolData> pool = new Dictionary<string, PoolData>();
        private GameObject poolObj;
        public GameObject GetObj(string name)
        {
            GameObject gameobj = null;

            if (pool.ContainsKey(name) && pool[name].poolList.Count > 0)
            {
                gameobj = pool[name].GetObj();
            }
            else
            {
                gameobj = ResourceManager.instance.Load<GameObject>(name);
                gameobj.name = name;
            }
            return gameobj;
        }

        public GameObject GetObj(string name, GameObject shotObj, Vector3 position, Quaternion quaternion)
        {
            GameObject gameobj = null;

            if (pool.ContainsKey(name) && pool[name].poolList.Count > 0)
            {
                gameobj = pool[name].GetObj();
                gameobj.transform.position = position;
                gameobj.transform.rotation = quaternion;
            }
            else
            {
                gameobj = GameObject.Instantiate(shotObj, position, quaternion);
                gameobj.name = name;
            }
            return gameobj;
        }

        public void GetObjAsync(string name, UnityAction<GameObject> callBack)
        {

            if (pool.ContainsKey(name) && pool[name].poolList.Count > 0)
            {
                callBack(pool[name].GetObj());
            }
            else
            {
                ResourceManager.instance.LoadAsync<GameObject>(name, (o) =>
                {
                    o.name = name;
                    callBack(o);
                });
            }
        }
        public void CheckExist(string name, UnityAction<GameObject> callBack)
        {

            if (pool.ContainsKey(name) && pool[name].poolList.Count > 0)
            {
                callBack(pool[name].GetObj());
            }
        }

        public void PushObj(string name, GameObject gameobj)
        {
            if (poolObj == null)
                poolObj = new GameObject("Pool");

            if (pool.ContainsKey(name))
            {
                pool[name].PushObj(gameobj);
            }
            else
            {
                pool.Add(name, new PoolData(gameobj, poolObj));
            }
        }

        public void Clear()
        {
            pool.Clear();
            poolObj = null;
        }

        public Dictionary<string, PoolData> GetPool()
        {
            return pool;
        }
    }
}