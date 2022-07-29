using System;
using UnityEngine;
using System.Collections.Generic;

namespace GameKit
{
    internal sealed class EntityInfo : IReference
    {
        private Type m_EntityLogicType;
        private Transform m_ParentTransform;
        private object m_UserData;

        public EntityInfo()
        {
            m_EntityLogicType = null;
            m_ParentTransform = null;
            m_UserData = null;
        }
        #region Properties
        public Type EntityLogicType
        {
            get
            {
                return m_EntityLogicType;
            }
        }
        public Transform ParentTransform
        {
            get
            {
                return m_ParentTransform;
            }
        }

        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }

        #endregion
        public static EntityInfo Create(Type entityLogicType, object userData)
        {
            EntityInfo showEntityInfo = ReferencePool.Acquire<EntityInfo>();
            showEntityInfo.m_EntityLogicType = entityLogicType;
            showEntityInfo.m_UserData = userData;
            return showEntityInfo;
        }

        public static EntityInfo Create(Transform parentTransform, object userData)
        {
            EntityInfo attachEntityInfo = ReferencePool.Acquire<EntityInfo>();
            attachEntityInfo.m_ParentTransform = parentTransform;
            attachEntityInfo.m_UserData = userData;
            return attachEntityInfo;
        }

        public void Clear()
        {
            m_EntityLogicType = null;
            m_UserData = null;
        }
    }
}
