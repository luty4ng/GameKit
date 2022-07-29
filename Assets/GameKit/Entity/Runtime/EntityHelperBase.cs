using UnityEngine;
namespace GameKit
{
    public abstract class EntityHelperBase : MonoBehaviour, IEntityHelper
    {
        public abstract object InstantiateEntity(object entityAsset);
        public abstract IEntity CreateEntity(object entityInstance, IEntityGroup entityGroup, object userData);
        public abstract void ReleaseEntity(object entityAsset, object entityInstance);
    }
}
