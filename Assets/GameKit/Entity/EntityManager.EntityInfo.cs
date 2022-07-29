using System.Collections.Generic;

namespace GameKit
{
    internal sealed partial class EntityManager : GameKitModule, IEntityManager
    {
        private sealed class EntityInfo : IReference
        {
            private int m_SerialId;
            private int m_EntityId;
            private EntityGroup m_EntityGroup;
            private object m_UserData;
            private IEntity m_Entity;
            private IEntity m_ParentEntity;
            private List<IEntity> m_ChildEntities;

            public EntityInfo()
            {
                m_SerialId = 0;
                m_EntityId = 0;
                m_EntityGroup = null;
                m_UserData = null;
                m_Entity = null;
                m_ParentEntity = null;
                m_ChildEntities = new List<IEntity>();
            }
            #region Properties
            public IEntity Entity
            {
                get
                {
                    return m_Entity;
                }
            }
            public IEntity ParentEntity
            {
                get
                {
                    return m_ParentEntity;
                }
                set
                {
                    m_ParentEntity = value;
                }
            }

            public int ChildEntityCount
            {
                get
                {
                    return m_ChildEntities.Count;
                }
            }

            public int SerialId
            {
                get
                {
                    return m_SerialId;
                }
            }

            public int EntityId
            {
                get
                {
                    return m_EntityId;
                }
            }

            public EntityGroup EntityGroup
            {
                get
                {
                    return m_EntityGroup;
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

            #region Public
            public IEntity GetChildEntity()
            {
                return m_ChildEntities.Count > 0 ? m_ChildEntities[0] : null;
            }

            public IEntity[] GetChildEntities()
            {
                return m_ChildEntities.ToArray();
            }

            public void GetChildEntities(List<IEntity> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (IEntity childEntity in m_ChildEntities)
                {
                    results.Add(childEntity);
                }
            }

            public void AddChildEntity(IEntity childEntity)
            {
                if (m_ChildEntities.Contains(childEntity))
                {
                    throw new GameKitException("Can not add child entity which is already exist.");
                }

                m_ChildEntities.Add(childEntity);
            }

            public void RemoveChildEntity(IEntity childEntity)
            {
                if (!m_ChildEntities.Remove(childEntity))
                {
                    throw new GameKitException("Can not remove child entity which is not exist.");
                }
            }
            #endregion

            public static EntityInfo Create(int serialId, int entityId, EntityGroup entityGroup, object userData)
            {
                EntityInfo showEntityInfo = ReferencePool.Acquire<EntityInfo>();
                showEntityInfo.m_SerialId = serialId;
                showEntityInfo.m_EntityId = entityId;
                showEntityInfo.m_EntityGroup = entityGroup;
                showEntityInfo.m_UserData = userData;
                return showEntityInfo;
            }

            public static EntityInfo Create(IEntity entity)
            {
                if (entity == null)
                {
                    throw new GameKitException("Entity is invalid.");
                }

                EntityInfo entityInfo = ReferencePool.Acquire<EntityInfo>();
                entityInfo.m_Entity = entity;
                return entityInfo;
            }

            public void Clear()
            {
                m_SerialId = 0;
                m_EntityId = 0;
                m_EntityGroup = null;
                m_UserData = null;
                m_Entity = null;
                m_ParentEntity = null;
                m_ChildEntities.Clear();
            }
        }
    }
}
