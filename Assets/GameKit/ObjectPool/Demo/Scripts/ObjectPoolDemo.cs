using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;

namespace GameKit.Demo
{
    public class ObjectPoolDemo : MonoBehaviour
    {
        public int poolSize = 16;
        public MonoObject monoObject_Prototype;
        public Queue<BaseObject> baseObject_Tests;
        private IObjectPool<BaseObject> baseObjectPool = null;
        void Start()
        {
            baseObject_Tests = new Queue<BaseObject>();
            baseObjectPool = GameKitComponentCenter.GetComponent<ObjectPoolComponent>().CreateSingleSpawnObjectPool<BaseObject>("Base Object", poolSize);
        }

        private void CreateObject()
        {
            MonoObject monoObject = null;
            BaseObject baseObject = baseObjectPool.Spawn();
            if (baseObject != null)
            {
                monoObject = (MonoObject)baseObject.Target;
            }
            else
            {
                monoObject = Instantiate(monoObject_Prototype);
                Transform transform = monoObject.GetComponent<Transform>();
                transform.localScale = Vector3.one;
                baseObject = BaseObject.Create(monoObject);
                baseObjectPool.Register(baseObject, true);
            }

            StartCoroutine(DelayedExcute(() =>
            {
                baseObjectPool.Unspawn(baseObject);
            }, 1f));
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CreateObject();
            }

            // if (Input.GetKeyDown(KeyCode.Q))
            // {
            //     baseObjectPool.Unspawn(baseObject_Tests.Dequeue());
            // }


        }

        private IEnumerator DelayedExcute(UnityAction action, float t)
        {
            yield return new WaitForSeconds(t);
            action?.Invoke();
        }

        private void UnspawnObject(BaseObject baseObject1)
        {
            baseObjectPool.Unspawn(baseObject1);
        }
    }

}

