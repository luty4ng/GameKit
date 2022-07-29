using GameKit;
using System;
using System.Collections.Generic;

namespace GameKit
{
    public interface IEntityManager
    {
        int EntityCount { get; }
        int EntityGroupCount { get; }
        void SetObjectPoolManager(IObjectPoolManager objectPoolManager);
        void SetEntityHelper(IEntityHelper entityHelper);
        bool HasEntityGroup(string entityGroupName);
        IEntityGroup GetEntityGroup(string entityGroupName);
        IEntityGroup[] GetAllEntityGroups();
        bool AddEntityGroup(string groupName, IEntityGroupHelper entityGroupHelper, float releaseInterval, int instanceCapacity, float instanceExpireTime, int instancePriority);
        bool HasEntity(int entityId);
        bool HasEntity(string entityAssetName);
        IEntity GetEntity(int entityId);
        IEntity GetEntity(string entityAssetName);
        IEntity[] GetEntities(string entityAssetName);
        IEntity[] GetAllLoadedEntities();
        void ShowEntity(int entityId, string entityAssetName, string entityGroupName, int priority = 0, object userData = null);
        void HideEntity(int entityId, object userData = null);
        void HideAllLoadedEntities(object userData = null);
        void HideAllLoadingEntities();
        bool IsLoadingEntity(int entityId);
        bool IsValidEntity(IEntity entity);
        int GetChildEntityCount(int parentEntityId);
        IEntity GetParentEntity(int childEntityId);
        IEntity GetChildEntity(int parentEntityId);
        IEntity[] GetChildEntities(int parentEntityId);
        void AttachEntity(int childEntityId, int parentEntityId, object userData = null);
        void DetachEntity(int childEntityId, object userData = null);
        void DetachChildEntities(int parentEntityId, object userData = null);
    }
}