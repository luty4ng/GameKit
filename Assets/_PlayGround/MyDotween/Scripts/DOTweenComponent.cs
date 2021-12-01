using UnityEngine;

namespace MyDOTween {
    public class DOTweenComponent : MonoBehaviour {
        internal static void Create() {
            if (DOTween.instance == null) {
                GameObject main = new GameObject("[DOTween]");
                DontDestroyOnLoad(main);
                DOTween.instance = main.AddComponent<DOTweenComponent>();
            }
        }

        internal static void DestroyInstance() {
            if (DOTween.instance != null) {
                Destroy(DOTween.instance.gameObject);
            }
            DOTween.instance = null;
        }

        // main loop
        private void Update() {
            if (TweenManager.hasActiveTweens()) {
                TweenManager.Update(Time.deltaTime);
            }
        }
    }
}