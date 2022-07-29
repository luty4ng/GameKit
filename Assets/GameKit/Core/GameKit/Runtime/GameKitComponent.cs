using UnityEngine;
namespace GameKit
{
    public abstract class GameKitComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            GameKitComponentCenter.RegisterComponent(this);
        }
    }
}
