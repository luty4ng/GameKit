using UnityEngine;
namespace GameKit
{
    public abstract class EntityBase : EntityLogic
    {
        [SerializeField]
        private EntityData m_EntityData = null;

        public int Id
        {
            get
            {
                return Entity.Id;
            }
        }

        public Animation CachedAnimation
        {
            get;
            private set;
        }

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            CachedAnimation = GetComponent<Animation>();
        }

        protected internal override void OnRecycle()
        {
            base.OnRecycle();
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_EntityData = userData as EntityData;
            if (m_EntityData == null)
            {
                Utility.Debugger.LogError("Entity data is invalid.");
                return;
            }

            Name = Utility.Text.Format("[Entity {0}]", Id);
            CachedTransform.localPosition = m_EntityData.Position;
            CachedTransform.localRotation = m_EntityData.Rotation;
            CachedTransform.localScale = Vector3.one;
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }

        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
        }

        protected internal override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
        }

        protected internal override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
        }

        protected internal override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            base.OnDetachFrom(parentEntity, userData);
        }

        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}
