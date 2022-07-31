using GameKit;
using System;
using System.Collections.Generic;

namespace GameKit
{
    internal sealed partial class EntityManager : GameKitModule, IEntityManager
    {
        private readonly Dictionary<int, EntityInfo> m_EntityInfos;
        private readonly Dictionary<string, EntityGroup> m_EntityGroups;
        private readonly Dictionary<int, int> m_EntitiesBeingLoaded;
        private readonly HashSet<int> m_EntitiesToReleaseOnLoad;
        private readonly Queue<EntityInfo> m_RecycleQueue;
        private IObjectPoolManager m_ObjectPoolManager;
        private IEntityHelper m_EntityHelper;
        private int m_Serial;
        private bool m_IsShutdown;

        public EntityManager()
        {
            m_EntityInfos = new Dictionary<int, EntityInfo>();
            m_EntityGroups = new Dictionary<string, EntityGroup>(StringComparer.Ordinal);
            m_EntitiesBeingLoaded = new Dictionary<int, int>();
            m_EntitiesToReleaseOnLoad = new HashSet<int>();
            m_RecycleQueue = new Queue<EntityInfo>();
            m_ObjectPoolManager = null;
            m_EntityHelper = null;
            m_Serial = 0;
            m_IsShutdown = false;
        }

        #region Properties
        public int EntityCount
        {
            get
            {
                return m_EntityInfos.Count;
            }
        }

        public int EntityGroupCount
        {
            get
            {
                return m_EntityGroups.Count;
            }
        }
        #endregion

        #region Functional
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            while (m_RecycleQueue.Count > 0)
            {
                EntityInfo entityInfo = m_RecycleQueue.Dequeue();
                IEntity entity = entityInfo.Entity;
                EntityGroup entityGroup = (EntityGroup)entity.EntityGroup;
                if (entityGroup == null)
                {
                    throw new GameKitException("Entity group is invalid.");
                }
                entity.OnRecycle();
                entityGroup.UnspawnEntity(entity);
                ReferencePool.Release(entityInfo);
            }

            foreach (KeyValuePair<string, EntityGroup> entityGroup in m_EntityGroups)
            {
                entityGroup.Value.Update(elapseSeconds, realElapseSeconds);
            }
        }

        internal override void Shutdown()
        {
            m_IsShutdown = true;
            HideAllLoadedEntities();
            m_EntityGroups.Clear();
            m_EntitiesBeingLoaded.Clear();
            m_EntitiesToReleaseOnLoad.Clear();
            m_RecycleQueue.Clear();
        }

        public void SetObjectPoolManager(IObjectPoolManager objectPoolManager)
        {
            if (objectPoolManager == null)
            {
                throw new GameKitException("Object pool manager is invalid.");
            }

            m_ObjectPoolManager = objectPoolManager;
        }

        public void SetEntityHelper(IEntityHelper entityHelper)
        {

            if (entityHelper == null)
            {
                throw new GameKitException("Entity helper is invalid.");
            }

            m_EntityHelper = entityHelper;

        }
        #endregion

        #region EntityGroup
        public bool HasEntityGroup(string entityGroupName)
        {
            if (string.IsNullOrEmpty(entityGroupName))
            {
                throw new GameKitException("Entity group name is invalid.");
            }

            return m_EntityGroups.ContainsKey(entityGroupName);
        }

        public IEntityGroup GetEntityGroup(string entityGroupName)
        {
            if (string.IsNullOrEmpty(entityGroupName))
            {
                throw new GameKitException("Entity group name is invalid.");
            }

            EntityGroup entityGroup = null;
            if (m_EntityGroups.TryGetValue(entityGroupName, out entityGroup))
            {
                return entityGroup;
            }

            return null;
        }

        public IEntityGroup[] GetAllEntityGroups()
        {
            int index = 0;
            IEntityGroup[] results = new IEntityGroup[m_EntityGroups.Count];
            foreach (KeyValuePair<string, EntityGroup> entityGroup in m_EntityGroups)
            {
                results[index++] = entityGroup.Value;
            }

            return results;
        }

        public void GetAllEntityGroups(List<IEntityGroup> results)
        {
            if (results == null)
            {
                throw new GameKitException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, EntityGroup> entityGroup in m_EntityGroups)
            {
                results.Add(entityGroup.Value);
            }
        }

        public bool AddEntityGroup(string entityGroupName, IEntityGroupHelper entityGroupHelper, float instanceAutoReleaseInterval, int instanceCapacity, float instanceExpireTime, int instancePriority)
        {
            if (string.IsNullOrEmpty(entityGroupName))
            {
                throw new GameKitException("Entity group name is invalid.");
            }

            if (m_ObjectPoolManager == null)
            {
                throw new GameKitException("You must set object pool manager first.");
            }

            if (HasEntityGroup(entityGroupName))
            {
                return false;
            }

            m_EntityGroups.Add(entityGroupName, new EntityGroup(entityGroupName, instanceAutoReleaseInterval, instanceCapacity, instanceExpireTime, instancePriority, entityGroupHelper, m_ObjectPoolManager));

            return true;
        }
        #endregion

        #region Entity
        public void DetachEntity(IEntity childEntity)
        {
            DetachEntity(childEntity, null);
        }

        public void DetachEntity(IEntity childEntity, object userData)
        {
            if (childEntity == null)
            {
                throw new GameKitException("Child entity is invalid.");
            }

            DetachEntity(childEntity.Id, userData);
        }

        public void DetachEntity(int childEntityId, object userData)
        {
            EntityInfo childEntityInfo = GetEntityInfo(childEntityId);
            if (childEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find child entity '{0}'.", childEntityId));
            }

            IEntity parentEntity = childEntityInfo.ParentEntity;
            if (parentEntity == null)
            {
                return;
            }

            EntityInfo parentEntityInfo = GetEntityInfo(parentEntity.Id);
            if (parentEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find parent entity '{0}'.", parentEntity.Id));
            }

            IEntity childEntity = childEntityInfo.Entity;
            childEntityInfo.ParentEntity = null;
            parentEntityInfo.RemoveChildEntity(childEntity);
            parentEntity.OnDetached(childEntity, userData);
            childEntity.OnDetachFrom(parentEntity, userData);
        }

        public bool HasEntity(int entityId)
        {
            return m_EntityInfos.ContainsKey(entityId);
        }
        public bool HasEntity(string entityAssetName)
        {
            if (string.IsNullOrEmpty(entityAssetName))
            {
                throw new GameKitException("Entity asset name is invalid.");
            }

            foreach (KeyValuePair<int, EntityInfo> entityInfo in m_EntityInfos)
            {
                if (entityInfo.Value.Entity.AssetName == entityAssetName)
                {
                    return true;
                }
            }
            return false;
        }

        public IEntity GetEntity(int entityId)
        {
            EntityInfo entityInfo = GetEntityInfo(entityId);
            if (entityInfo == null)
            {
                return null;
            }

            return entityInfo.Entity;
        }

        public IEntity GetEntity(string entityAssetName)
        {
            if (string.IsNullOrEmpty(entityAssetName))
            {
                throw new GameKitException("Entity asset name is invalid.");
            }

            foreach (KeyValuePair<int, EntityInfo> entityInfo in m_EntityInfos)
            {
                if (entityInfo.Value.Entity.AssetName == entityAssetName)
                {
                    return entityInfo.Value.Entity;
                }
            }

            return null;
        }

        public IEntity[] GetEntities(string entityAssetName)
        {
            if (string.IsNullOrEmpty(entityAssetName))
            {
                throw new GameKitException("Entity asset name is invalid.");
            }

            List<IEntity> results = new List<IEntity>();
            foreach (KeyValuePair<int, EntityInfo> entityInfo in m_EntityInfos)
            {
                if (entityInfo.Value.Entity.AssetName == entityAssetName)
                {
                    results.Add(entityInfo.Value.Entity);
                }
            }

            return results.ToArray();
        }
        public IEntity[] GetAllLoadedEntities()
        {
            int index = 0;
            IEntity[] results = new IEntity[m_EntityInfos.Count];
            foreach (KeyValuePair<int, EntityInfo> entityInfo in m_EntityInfos)
            {
                results[index++] = entityInfo.Value.Entity;
            }

            return results;
        }

        public int[] GetAllLoadingEntityIds()
        {
            int index = 0;
            int[] results = new int[m_EntitiesBeingLoaded.Count];
            foreach (KeyValuePair<int, int> entityBeingLoaded in m_EntitiesBeingLoaded)
            {
                results[index++] = entityBeingLoaded.Key;
            }

            return results;
        }

        public void HideAllLoadedEntities()
        {
            HideAllLoadedEntities(null);
        }
        public void HideAllLoadedEntities(object userData)
        {
            while (m_EntityInfos.Count > 0)
            {
                foreach (KeyValuePair<int, EntityInfo> entityInfo in m_EntityInfos)
                {
                    InternalHideEntity(entityInfo.Value, userData);
                    break;
                }
            }
        }

        public void HideAllLoadingEntities()
        {
            foreach (KeyValuePair<int, int> entityBeingLoaded in m_EntitiesBeingLoaded)
            {
                m_EntitiesToReleaseOnLoad.Add(entityBeingLoaded.Value);
            }

            m_EntitiesBeingLoaded.Clear();
        }
        public void HideEntity(int entityId, object userData)
        {
            if (IsLoadingEntity(entityId))
            {
                m_EntitiesToReleaseOnLoad.Add(m_EntitiesBeingLoaded[entityId]);
                m_EntitiesBeingLoaded.Remove(entityId);
                return;
            }

            EntityInfo entityInfo = GetEntityInfo(entityId);
            if (entityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find entity '{0}'.", entityId));
            }

            InternalHideEntity(entityInfo, userData);
        }

        public IEntity GetParentEntity(int childEntityId)
        {
            EntityInfo childEntityInfo = GetEntityInfo(childEntityId);
            if (childEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find child entity '{0}'.", childEntityId));
            }

            return childEntityInfo.ParentEntity;
        }

        public int GetChildEntityCount(int parentEntityId)
        {
            EntityInfo parentEntityInfo = GetEntityInfo(parentEntityId);
            if (parentEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find parent entity '{0}'.", parentEntityId));
            }

            return parentEntityInfo.ChildEntityCount;
        }
        public IEntity GetChildEntity(int parentEntityId)
        {
            EntityInfo parentEntityInfo = GetEntityInfo(parentEntityId);
            if (parentEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find parent entity '{0}'.", parentEntityId));
            }

            return parentEntityInfo.GetChildEntity();
        }

        public IEntity[] GetChildEntities(int parentEntityId)
        {
            EntityInfo parentEntityInfo = GetEntityInfo(parentEntityId);
            if (parentEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find parent entity '{0}'.", parentEntityId));
            }

            return parentEntityInfo.GetChildEntities();
        }
        public bool IsLoadingEntity(int entityId)
        {
            return m_EntitiesBeingLoaded.ContainsKey(entityId);
        }

        public bool IsValidEntity(IEntity entity)
        {
            if (entity == null)
            {
                return false;
            }

            return HasEntity(entity.Id);
        }

        public void AttachEntity(int childEntityId, int parentEntityId, object userData)
        {
            if (childEntityId == parentEntityId)
            {
                throw new GameKitException(Utility.Text.Format("Can not attach entity when child entity id equals to parent entity id '{0}'.", parentEntityId));
            }

            EntityInfo childEntityInfo = GetEntityInfo(childEntityId);
            if (childEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find child entity '{0}'.", childEntityId));
            }

            EntityInfo parentEntityInfo = GetEntityInfo(parentEntityId);
            if (parentEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find parent entity '{0}'.", parentEntityId));
            }

            IEntity childEntity = childEntityInfo.Entity;
            IEntity parentEntity = parentEntityInfo.Entity;
            DetachEntity(childEntity.Id, userData);
            childEntityInfo.ParentEntity = parentEntity;
            parentEntityInfo.AddChildEntity(childEntity);
            parentEntity.OnAttached(childEntity, userData);
            childEntity.OnAttachTo(parentEntity, userData);
        }

        public void DetachChildEntities(int parentEntityId, object userData)
        {
            EntityInfo parentEntityInfo = GetEntityInfo(parentEntityId);
            if (parentEntityInfo == null)
            {
                throw new GameKitException(Utility.Text.Format("Can not find parent entity '{0}'.", parentEntityId));
            }

            while (parentEntityInfo.ChildEntityCount > 0)
            {
                IEntity childEntity = parentEntityInfo.GetChildEntity();
                DetachEntity(childEntity.Id, userData);
            }
        }

        public void ShowEntity(int entityId, string entityAssetName, string entityGroupName, int priority, object userData)
        {
            if (m_EntityHelper == null)
            {
                throw new GameKitException("You must set entity helper first.");
            }

            if (string.IsNullOrEmpty(entityAssetName))
            {
                throw new GameKitException("Entity asset name is invalid.");
            }

            if (string.IsNullOrEmpty(entityGroupName))
            {
                throw new GameKitException("Entity group name is invalid.");
            }

            if (HasEntity(entityId))
            {
                throw new GameKitException(Utility.Text.Format("Entity id '{0}' is already exist.", entityId));
            }

            if (IsLoadingEntity(entityId))
            {
                throw new GameKitException(Utility.Text.Format("Entity '{0}' is already being loaded.", entityId));
            }

            EntityGroup entityGroup = (EntityGroup)GetEntityGroup(entityGroupName);
            if (entityGroup == null)
            {
                throw new GameKitException(Utility.Text.Format("Entity group '{0}' is not exist.", entityGroupName));
            }

            EntityObject entityObject = entityGroup.SpawnEntityObject(entityAssetName);
            if (entityObject == null)
            {
                int serialId = ++m_Serial;
                m_EntitiesBeingLoaded.Add(entityId, serialId);
                // m_ResourceManager.LoadAsset(entityAssetName, priority, m_LoadAssetCallbacks, EntityInfo.Create(serialId, entityId, entityGroup, userData));
                ResourceManager.instance.GetAssetAsyn(entityAssetName, (UnityEngine.Object obj) =>
                {
                    LoadAssetSuccessCallback(entityAssetName, obj, 0f, userData);
                },
                () =>
                {
                    LoadAssetFailureCallback(entityAssetName, "Load Fail", userData);
                });
                return;
            }

            InternalShowEntity(entityId, entityAssetName, entityGroup, entityObject.Target, 0f, userData);
        }
        #endregion


        #region Private
        private void InternalHideEntity(EntityInfo entityInfo, object userData)
        {
            while (entityInfo.ChildEntityCount > 0)
            {
                IEntity childEntity = entityInfo.GetChildEntity();
                HideEntity(childEntity.Id, userData);
            }

            IEntity entity = entityInfo.Entity;
            DetachEntity(entity.Id, userData);
            entity.OnHide(m_IsShutdown, userData);

            EntityGroup entityGroup = (EntityGroup)entity.EntityGroup;
            if (entityGroup == null)
            {
                throw new GameKitException("Entity group is invalid.");
            }

            entityGroup.RemoveEntity(entity);
            if (!m_EntityInfos.Remove(entity.Id))
            {
                throw new GameKitException("Entity info is unmanaged.");
            }
            m_RecycleQueue.Enqueue(entityInfo);
        }

        private EntityInfo GetEntityInfo(int entityId)
        {
            EntityInfo entityInfo = null;
            if (m_EntityInfos.TryGetValue(entityId, out entityInfo))
            {
                return entityInfo;
            }
            return null;
        }
        private void InternalShowEntity(int entityId, string entityAssetName, EntityGroup entityGroup, object entityInstance, float duration, object userData)
        {
            try
            {
                IEntity entity = m_EntityHelper.CreateEntity(entityInstance, entityGroup, userData);
                if (entity == null)
                    throw new GameKitException("Can not create entity in entity helper.");

                EntityInfo entityInfo = EntityInfo.Create(entity);
                m_EntityInfos.Add(entityId, entityInfo);
                entity.OnInit(entityId, entityAssetName, entityGroup, userData);
                entityGroup.AddEntity(entity);
                entity.OnShow(userData);

                EntityShowSuccessEventArgs eventArgs = EntityShowSuccessEventArgs.Create(entity, duration, userData);
                EventManager.instance.EventTrigger<EntityShowSuccessEventArgs>(eventArgs.Id, eventArgs);
                ReferencePool.Release(eventArgs);
            }
            catch (Exception exception)
            {
                EntityShowFailEventArgs eventArgs = EntityShowFailEventArgs.Create(entityId, entityAssetName, entityGroup.Name, exception.ToString(), userData);
                EventManager.instance.EventTrigger<EntityShowFailEventArgs>(eventArgs.Id, eventArgs);
                ReferencePool.Release(eventArgs);
                throw new GameKitException("Internal Show Entity Fail.", exception);
            }
        }

        private void LoadAssetSuccessCallback(string entityAssetName, object entityAsset, float duration, object userData)
        {
            EntityInfo entityInfo = (EntityInfo)userData;
            if (entityInfo == null)
            {
                throw new GameKitException("Show entity info is invalid.");
            }

            if (m_EntitiesToReleaseOnLoad.Contains(entityInfo.SerialId))
            {
                m_EntitiesToReleaseOnLoad.Remove(entityInfo.SerialId);
                ReferencePool.Release(entityInfo);
                m_EntityHelper.ReleaseEntity(entityAsset, null);
                return;
            }

            m_EntitiesBeingLoaded.Remove(entityInfo.EntityId);
            EntityObject entityObject = EntityObject.Create(entityAssetName, entityAsset, m_EntityHelper.InstantiateEntity(entityAsset), m_EntityHelper);
            entityInfo.EntityGroup.RegisterEntityObject(entityObject, true);

            InternalShowEntity(entityInfo.EntityId, entityAssetName, entityInfo.EntityGroup, entityObject.Target, duration, entityInfo.UserData);
            ReferencePool.Release(entityInfo);
        }

        private void LoadAssetFailureCallback(string entityAssetName, string errorMessage, object userData)
        {
            EntityInfo entityInfo = (EntityInfo)userData;
            if (entityInfo == null)
            {
                throw new GameKitException("Show entity info is invalid.");
            }

            if (m_EntitiesToReleaseOnLoad.Contains(entityInfo.SerialId))
            {
                m_EntitiesToReleaseOnLoad.Remove(entityInfo.SerialId);
                return;
            }

            m_EntitiesBeingLoaded.Remove(entityInfo.EntityId);
            string appendErrorMessage = Utility.Text.Format("Load entity failure, asset name '{0}', error message '{2}'.", entityAssetName, errorMessage);
            EntityShowFailEventArgs entityShowFailEventArgs = EntityShowFailEventArgs.Create(entityInfo.EntityId, entityAssetName, entityInfo.EntityGroup.Name, appendErrorMessage, entityInfo.UserData);
            EventManager.instance.EventTrigger<EntityShowFailEventArgs>(entityShowFailEventArgs.Id, entityShowFailEventArgs);
            ReferencePool.Release(entityShowFailEventArgs);
        }
        #endregion
    }
}