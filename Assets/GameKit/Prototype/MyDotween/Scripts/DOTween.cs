using UnityEngine;

namespace MyDOTween {
    public static class DOTween {
        // main instance
        public static DOTweenComponent instance;

        // default ease type
        public static Ease defaultEaseType = Ease.Linear;

        // whether or not DOTween is initialized
        internal static bool initialized = false;

        // =========================== [Init] ===========================

        public static void Init(Ease? easeTypeByDefault = null) {
            if (initialized) {
                return;
            }
            if (easeTypeByDefault != null) {
                // assign setting
                DOTween.defaultEaseType = (Ease)easeTypeByDefault;
            }
            // create main instance
            DOTweenComponent.Create();
            initialized = true;
        }

        private static void AutoInit() {
            Init(null);
        }

        private static void InitCheck() {
            if (!initialized) {
                AutoInit();
            }
        }

        // =========================== [Apply To] ===========================

        private static Tweener<TweenVT, StoreVT> ApplyTo<TweenVT, StoreVT>(
            DOGetter<TweenVT> getter, DOSetter<TweenVT> setter,
            StoreVT endValue, float duration
        ) {
            // check init
            InitCheck();
            // create tweener
            Tweener<TweenVT, StoreVT> tweener = TweenManager.GetTweener<TweenVT, StoreVT>();
            // setup tweener
            bool setupOk = tweener.Setup(getter, setter, endValue, duration);
            if (setupOk) {
                return tweener;
            }
            else {
                return null;
            }
        }

        // =========================== [To] ===========================

        // Vector3
        public static Tweener<Vector3, Vector3> TO(
            DOGetter<Vector3> getter, DOSetter<Vector3> setter,
            Vector3 endValue, float duration
        ) {
            return ApplyTo<Vector3, Vector3>(getter, setter, endValue, duration);
        }

        // Color
        public static Tweener<Color, Color> TO(
            DOGetter<Color> getter, DOSetter<Color> setter,
            Color endValue, float duration
        ) {
            return ApplyTo<Color, Color>(getter, setter, endValue, duration);
        }

        // =========================== [Sequence] ===========================

        public static Sequence Sequence() {
            InitCheck();
            Sequence sequence = TweenManager.GetSequence();
            return sequence;
        }
    }
}