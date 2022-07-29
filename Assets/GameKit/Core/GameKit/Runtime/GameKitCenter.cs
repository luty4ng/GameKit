using UnityEngine;

namespace GameKit
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
            Core = GameKit.GameKitComponentCenter.GetComponent<GameKitCoreComponent>();
            Fsm = GameKit.GameKitComponentCenter.GetComponent<FsmComponent>();
        }
    }
}

