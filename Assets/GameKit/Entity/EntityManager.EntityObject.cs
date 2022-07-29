using GameKit;
using System;
using System.Collections.Generic;

namespace GameKit
{
    internal sealed partial class EntityManager
    {
        public sealed class EntityObject : ObjectBase
        {
            private object m_EntityAsset;
            private IEntityHelper m_EntityHelper;

            public EntityObject()
            {
                m_EntityAsset = null;
                m_EntityHelper = null;
            }

            public static EntityObject Create(string name, object entityAsset, object entityInstance, IEntityHelper entityHelper)
            {
                if (entityAsset == null)
                {
                    throw new GameKitException("Entity asset is invalid.");
                }

                if (entityHelper == null)
                {
                    throw new GameKitException("Entity helper is invalid.");
                }

                EntityObject entityInstanceObject = ReferencePool.Acquire<EntityObject>();
                entityInstanceObject.Initialize(name, entityInstance);
                entityInstanceObject.m_EntityAsset = entityAsset;
                entityInstanceObject.m_EntityHelper = entityHelper;
                return entityInstanceObject;
            }

            public override void Clear()
            {
                base.Clear();
                m_EntityAsset = null;
                m_EntityHelper = null;
            }

            protected internal override void Release(bool isShutdown)
            {
                m_EntityHelper.ReleaseEntity(m_EntityAsset, Target);
            }
        }
    }
}