using GameKit;
using System;
using System.Collections.Generic;
using GameKit.DataStructure;

namespace GameKit
{
    internal sealed partial class EntityManager
    {
        public sealed class EntityGroup : IEntityGroup
        {
            private readonly string m_Name;
            private readonly IEntityGroupHelper m_EntityGroupHelper;
            private readonly IObjectPool<EntityObject> m_InstancePool;
            private readonly CachedLinkedList<IEntity> m_Entities;
            private LinkedListNode<IEntity> m_CachedNode;

            public EntityGroup(string name, float instanceAutoReleaseInterval, int instanceCapacity, float instanceExpireTime, int instancePriority, IEntityGroupHelper entityGroupHelper, IObjectPoolManager objectPoolManager)
            {
                if (string.IsNullOrEmpty(name))
                {
                    throw new GameKitException("Entity group name is invalid.");
                }

                m_Name = name;
                m_InstancePool = objectPoolManager.CreateSingleSpawnObjectPool<EntityObject>(Utility.Text.Format("Entity Instance Pool ({0})", name), instanceCapacity, instanceExpireTime, instancePriority);
                m_InstancePool.AutoReleaseInterval = instanceAutoReleaseInterval;
                m_EntityGroupHelper = entityGroupHelper;
                m_Entities = new CachedLinkedList<IEntity>();
                m_CachedNode = null;
            }


            #region Properties
            public string Name
            {
                get
                {
                    return m_Name;
                }
            }

            public int EntityCount
            {
                get
                {
                    return m_Entities.Count;
                }
            }

            public float InstanceReleaseInterval
            {
                get
                {
                    return m_InstancePool.AutoReleaseInterval;
                }
                set
                {
                    m_InstancePool.AutoReleaseInterval = value;
                }
            }

            public int InstanceCapacity
            {
                get
                {
                    return m_InstancePool.Capacity;
                }
                set
                {
                    m_InstancePool.Capacity = value;
                }
            }

            public float InstanceExpireTime
            {
                get
                {
                    return m_InstancePool.ExpireTime;
                }
                set
                {
                    m_InstancePool.ExpireTime = value;
                }
            }

            public int InstancePriority
            {
                get
                {
                    return m_InstancePool.Priority;
                }
                set
                {
                    m_InstancePool.Priority = value;
                }
            }

            public IEntityGroupHelper Helper
            {
                get
                {
                    return m_EntityGroupHelper;
                }
            }
            
            #endregion

            public void Update(float elapseSeconds, float realElapseSeconds)
            {
                LinkedListNode<IEntity> current = m_Entities.First;
                while (current != null)
                {
                    m_CachedNode = current.Next;
                    current.Value.OnUpdate(elapseSeconds, realElapseSeconds);
                    current = m_CachedNode;
                    m_CachedNode = null;
                }
            }

            public bool HasEntity(int entityId)
            {
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.Id == entityId)
                    {
                        return true;
                    }
                }

                return false;
            }

            public bool HasEntity(string entityAssetName)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameKitException("Entity asset name is invalid.");
                }

                foreach (IEntity entity in m_Entities)
                {
                    if (entity.AssetName == entityAssetName)
                    {
                        return true;
                    }
                }

                return false;
            }

            public IEntity GetEntity(int entityId)
            {
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.Id == entityId)
                    {
                        return entity;
                    }
                }

                return null;
            }

            public IEntity GetEntity(string entityAssetName)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameKitException("Entity asset name is invalid.");
                }

                foreach (IEntity entity in m_Entities)
                {
                    if (entity.AssetName == entityAssetName)
                    {
                        return entity;
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
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.AssetName == entityAssetName)
                    {
                        results.Add(entity);
                    }
                }

                return results.ToArray();
            }

            public void GetEntities(string entityAssetName, List<IEntity> results)
            {
                if (string.IsNullOrEmpty(entityAssetName))
                {
                    throw new GameKitException("Entity asset name is invalid.");
                }

                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (IEntity entity in m_Entities)
                {
                    if (entity.AssetName == entityAssetName)
                    {
                        results.Add(entity);
                    }
                }
            }

            public IEntity[] GetAllEntities()
            {
                List<IEntity> results = new List<IEntity>();
                foreach (IEntity entity in m_Entities)
                {
                    results.Add(entity);
                }

                return results.ToArray();
            }

            public void GetAllEntities(List<IEntity> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (IEntity entity in m_Entities)
                {
                    results.Add(entity);
                }
            }

            public void AddEntity(IEntity entity)
            {
                m_Entities.AddLast(entity);
            }

            public void RemoveEntity(IEntity entity)
            {
                if (m_CachedNode != null && m_CachedNode.Value == entity)
                {
                    m_CachedNode = m_CachedNode.Next;
                }

                if (!m_Entities.Remove(entity))
                {
                    throw new GameKitException(Utility.Text.Format("Entity group '{0}' not exists specified entity '[{1}]{2}'.", m_Name, entity.Id, entity.AssetName));
                }
            }

            public void RegisterEntityObject(EntityObject obj, bool spawned)
            {
                m_InstancePool.Register(obj, spawned);
            }

            public EntityObject SpawnEntityObject(string name)
            {
                return m_InstancePool.Spawn(name);
            }

            public void UnspawnEntity(IEntity entity)
            {
                m_InstancePool.Unspawn(entity.Instance);
            }

            public void SetEntityInstanceLocked(object entityInstance, bool locked)
            {
                if (entityInstance == null)
                {
                    throw new GameKitException("Entity instance is invalid.");
                }

                m_InstancePool.SetLocked(entityInstance, locked);
            }

            public void SetEntityInstancePriority(object entityInstance, int priority)
            {
                if (entityInstance == null)
                {
                    throw new GameKitException("Entity instance is invalid.");
                }

                m_InstancePool.SetPriority(entityInstance, priority);
            }
        }
    }
}