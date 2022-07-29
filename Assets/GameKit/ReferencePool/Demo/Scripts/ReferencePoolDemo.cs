using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.DataStructure;

namespace GameKit.Demo
{
    public class ReferencePoolDemo : MonoBehaviour
    {
        public class CustomClass : IReference
        {
            string name;
            public CustomClass()
            {

            }

            public void Clear()
            {

            }
        }
        private void Start()
        {
            StartCoroutine(AcquirePerSec(1));
        }

        IEnumerator AcquirePerSec(float time)
        {
            ReferencePool.Acquire<CustomClass>();
            Debug.Log("First Acquire Reference Count: " + ReferencePool.Count);
            yield return new WaitForSeconds(time);
            ReferencePool.Acquire<CustomClass>();
            Debug.Log("Second Acquire Reference Count: " + ReferencePool.Count);
        }
    }
}
