namespace GameKit
{
    public interface IEntity
    {
        int Id { get; }
        string AssetName { get; }
        object Instance { get; }
        IEntityGroup EntityGroup { get; }
        void OnInit(int entityId, string name, IEntityGroup entityGroup, object userData);
        void OnRecycle();
        void OnShow(object userData);
        void OnHide(bool isShutdown, object userData);
        void OnAttached(IEntity childEntity, object userData);
        void OnDetached(IEntity childEntity, object userData);
        void OnAttachTo(IEntity parentEntity, object userData);
        void OnDetachFrom(IEntity parentEntity, object userData);
        void OnUpdate(float elapseSeconds, float realElapseSeconds);
    }
}