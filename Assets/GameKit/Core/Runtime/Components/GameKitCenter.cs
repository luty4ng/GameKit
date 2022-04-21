using UnityEngine;

namespace GameKit.UnityEngine
{
    public class GameKitCenter : MonoBehaviour
    {
        public static GameKitCoreComponent Core { get; private set; }
        public static FsmComponent Fsm { get; private set; }

        private void Start()
        {
            InitComponents();
        }

        private static void InitComponents()
        {
            Core = GameKit.UnityEngine.GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            Fsm = GameKit.UnityEngine.GameKitComponentCenter.GetComponent<FsmComponent>();
        }
    }
}

