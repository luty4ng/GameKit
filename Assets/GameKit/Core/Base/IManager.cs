namespace GameKit
{
    public interface IManager
    {
        bool IsActive { get; }
        void Enable();
        void Disable();
        void ShutDown();
        void Clear();
    }
}
