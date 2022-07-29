using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace GameKit.Demo
{
    public class BaseObject : ObjectBase
    {
        public static BaseObject Create(object target)
        {
            BaseObject baseObject = ReferencePool.Acquire<BaseObject>();
            baseObject.Initialize(target);
            return baseObject;
        }

        protected internal override void Release(bool isShutdown)
        {
            MonoObject monoObject = (MonoObject)Target;
            if (monoObject == null)
                return;
            Object.Destroy(monoObject.gameObject);
        }

        protected internal override void OnSpawn()
        {
            base.OnSpawn();
            MonoObject monoObject = (MonoObject)Target;
            monoObject.gameObject.SetActive(true);
        }

        protected internal override void OnUnspawn()
        {
            base.OnUnspawn();
            MonoObject monoObject = (MonoObject)Target;
            monoObject.gameObject.SetActive(false);
        }
    }

}

