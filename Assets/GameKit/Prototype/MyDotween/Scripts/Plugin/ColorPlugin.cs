using UnityEngine;

namespace MyDOTween {
    public class ColorPlugin : TweenPlugin<Color, Color> {
        // set tweener's startValue and changeValue
        public override void SetValues(Tweener<Color, Color> tweener) {
            tweener.startValue = tweener.getter();
            tweener.changeValue = tweener.endValue - tweener.startValue;
        }

        // evaluate and apply to tweener
        public override void EvaluateAndApply(
            Tweener<Color, Color> tweener, DOSetter<Color> setter,
            float elapsed, float duration,
            Color startValue, Color changeValue
        ) {
            // get ease value
            float easeVal = EaseManager.Evaluate(tweener.easeType, elapsed, duration);
            // set new value
            setter(startValue + changeValue * easeVal);
        }
    }
}