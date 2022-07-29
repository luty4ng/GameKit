using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit
{
    public abstract class EntityLogic : MonoBehaviour
    {
        private bool m_Available = false;
        private bool m_Visible = false;
        private Entity m_Entity = null;
        private Transform m_CachedTransform = null;
        private Transform m_OriginalTransform = null;
        private int m_OriginalLayer = 0;

        public Entity Entity
        {
            get
            {
                return m_Entity;
            }
        }

        public string Name
        {
            get
            {
                return gameObject.name;
            }
            set
            {
                gameObject.name = value;
            }
        }

        public bool Available
        {
            get
            {
                return m_Available;
            }
        }

        public bool Visible
        {
            get
            {
                return m_Available && m_Visible;
            }
            set
            {
                if (!m_Available)
                {
                    // Log.Warning("Entity '{0}' is not available.", Name);
                    return;
                }

                if (m_Visible == value)
                {
                    return;
                }

                m_Visible = value;
                InternalSetVisible(value);
            }
        }

        public Transform CachedTransform
        {
            get
            {
                return m_CachedTransform;
            }
        }

        protected internal virtual void OnInit(object userData)
        {
            if (m_CachedTransform == null)
            {
                m_CachedTransform = transform;
            }

            m_Entity = GetComponent<Entity>();
            m_OriginalLayer = gameObject.layer;
            m_OriginalTransform = CachedTransform.parent;
        }

        protected internal virtual void OnRecycle()
        {
        }

        protected internal virtual void OnShow(object userData)
        {
            m_Available = true;
            Visible = true;
        }

        protected internal virtual void OnHide(bool isShutdown, object userData)
        {
            gameObject.SetLayerRecursively(m_OriginalLayer);
            Visible = false;
            m_Available = false;
        }

        protected internal virtual void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
        }

        protected internal virtual void OnDetached(EntityLogic childEntity, object userData)
        {
        }

        protected internal virtual void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
        {
            CachedTransform.SetParent(parentTransform);
        }

        protected internal virtual void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            CachedTransform.SetParent(m_OriginalTransform);
        }

        protected internal virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        protected virtual void InternalSetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }

}
