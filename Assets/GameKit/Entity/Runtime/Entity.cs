using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace GameKit
{
    public class Entity : MonoBehaviour, IEntity
    {
        private int m_Id;
        private string m_AssetName;
        private IEntityGroup m_EntityGroup;
        private EntityLogic m_EntityLogic;

        #region Fields
        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        public string AssetName
        {
            get
            {
                return m_AssetName;
            }
        }

        public object Instance
        {
            get
            {
                return gameObject;
            }
        }

        public IEntityGroup EntityGroup
        {
            get
            {
                return m_EntityGroup;
            }
        }

        public EntityLogic Logic
        {
            get
            {
                return m_EntityLogic;
            }
        }
        #endregion

        public void OnInit(int entityId, string entityAssetName, IEntityGroup entityGroup, object userData)
        {
            m_Id = entityId;
            m_AssetName = entityAssetName;
            m_EntityGroup = entityGroup;

            EntityInfo showEntityInfo = (EntityInfo)userData;
            System.Type entityLogicType = showEntityInfo.EntityLogicType;
            if (entityLogicType == null)
            {
                Utility.Debugger.LogError("Entity logic type is invalid.");
                return;
            }

            if (m_EntityLogic != null)
            {
                if (m_EntityLogic.GetType() == entityLogicType)
                {
                    m_EntityLogic.enabled = true;
                    return;
                }

                Destroy(m_EntityLogic);
                m_EntityLogic = null;
            }

            m_EntityLogic = gameObject.AddComponent(entityLogicType) as EntityLogic;
            if (m_EntityLogic == null)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '{0}' can not add entity logic.", entityAssetName));
                return;
            }

            try
            {
                m_EntityLogic.OnInit(userData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogFail(Utility.Text.Format("Entity '[{0}]{1}' OnInit with exception '{2}'.", m_Id, AssetName, exception));
            }
        }

        public void OnRecycle()
        {
            try
            {
                m_EntityLogic.OnRecycle();
                m_EntityLogic.enabled = false;
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnRecycle with exception '{2}'.", m_Id, m_AssetName, exception));
            }

            m_Id = 0;
        }

        public void OnShow(object userData)
        {
            EntityInfo showEntityInfo = (EntityInfo)userData;
            try
            {
                m_EntityLogic.OnShow(showEntityInfo.UserData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnShow with exception '{2}'.", m_Id, m_AssetName, exception));
            }
        }

        public void OnHide(bool isShutdown, object userData)
        {
            try
            {
                m_EntityLogic.OnHide(isShutdown, userData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnHide with exception '{2}'.", m_Id, m_AssetName, exception));
            }
        }

        public void OnAttached(IEntity childEntity, object userData)
        {
            EntityInfo attachEntityInfo = (EntityInfo)userData;
            try
            {
                m_EntityLogic.OnAttached(((Entity)childEntity).Logic, attachEntityInfo.ParentTransform, attachEntityInfo.UserData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnAttached with exception '{2}'.", m_Id, m_AssetName, exception));
            }
        }

        public void OnDetached(IEntity childEntity, object userData)
        {
            try
            {
                m_EntityLogic.OnDetached(((Entity)childEntity).Logic, userData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnDetached with exception '{2}'.", m_Id, m_AssetName, exception));
            }
        }

        public void OnAttachTo(IEntity parentEntity, object userData)
        {
            EntityInfo attachEntityInfo = (EntityInfo)userData;
            try
            {
                m_EntityLogic.OnAttachTo(((Entity)parentEntity).Logic, attachEntityInfo.ParentTransform, attachEntityInfo.UserData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnAttachTo with exception '{2}'.", m_Id, m_AssetName, exception));
            }

            ReferencePool.Release(attachEntityInfo);
        }

        public void OnDetachFrom(IEntity parentEntity, object userData)
        {
            try
            {
                m_EntityLogic.OnDetachFrom(((Entity)parentEntity).Logic, userData);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnDetachFrom with exception '{2}'.", m_Id, m_AssetName, exception));
            }
        }

        public void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            try
            {
                m_EntityLogic.OnUpdate(elapseSeconds, realElapseSeconds);
            }
            catch (Exception exception)
            {
                Utility.Debugger.LogError(Utility.Text.Format("Entity '[{0}]{1}' OnUpdate with exception '{2}'.", m_Id, m_AssetName, exception));
            }
        }

    }
}


