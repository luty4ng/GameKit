
namespace GameKit
{
    internal abstract class GameKitModule
    {
        internal virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);
        internal abstract void Shutdown();
    }
}
