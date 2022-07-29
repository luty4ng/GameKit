using UnityEngine;
namespace GameKit
{
    public class EntityShowSuccessEventArgs : GameKitEventArgs
    {
        private const string m_id = "ENTITY_SHOW_SUCCESS";
        public override string Id
        {
            get
            {
                return m_id;
            }
        }

        public EntityShowSuccessEventArgs()
        {
            Entity = null;
            Duration = 0f;
            UserData = null;
        }

        public IEntity Entity
        {
            get;
            private set;
        }

        public float Duration
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public static EntityShowSuccessEventArgs Create(IEntity entity, float duration, object userData)
        {
            EntityShowSuccessEventArgs entityShowSuccessEventArgs = ReferencePool.Acquire<EntityShowSuccessEventArgs>();
            entityShowSuccessEventArgs.Entity = entity;
            entityShowSuccessEventArgs.Duration = duration;
            entityShowSuccessEventArgs.UserData = userData;
            return entityShowSuccessEventArgs;
        }

        public override void Clear()
        {
            Entity = null;
            Duration = 0f;
            UserData = null;
        }
    }
}

