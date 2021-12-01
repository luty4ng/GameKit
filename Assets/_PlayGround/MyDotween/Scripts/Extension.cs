using UnityEngine;

namespace MyDOTween {
    public static class Extensions {
        // =========================== [Do Action] ===========================

        public static Tweener<Vector3, Vector3> DOMove(
            this Transform target, Vector3 endValue, float duration
        ) {
            return DOTween.TO(() => target.position, p => target.position = p, endValue, duration);
        }

        public static Tweener<Vector3, Vector3> DOScale(
            this Transform target, Vector3 endValue, float duration
        ) {
            return DOTween.TO(() => target.localScale, s => target.localScale = s, endValue, duration);
        }

        public static Tweener<Color, Color> DOColor(
            this Material target, Color endValue, float duration
        ) {
            return DOTween.TO(() => target.color, c => target.color = c, endValue, duration);
        }

        // =========================== [For Tweener] ===========================

        public static Tweener<TweenVT, StoreVT> SetEase<TweenVT, StoreVT>(
            this Tweener<TweenVT, StoreVT> tweener, Ease ease
        ) {
            if (tweener != null && tweener.active) {
                tweener.easeType = ease;
            }
            return tweener;
        }

        // =========================== [For Sequence] ===========================

        public static Sequence Append(this Sequence sequence, Tween tween) {
            if (sequence == null || !sequence.active) {
                return sequence;
            }
            if (tween == null || !tween.active) {
                return sequence;
            }
            Sequence.DoInsert(sequence, tween);
            return sequence;
        }
    }
}