using UnityEngine;

namespace MyDOTween
{
    public class Vector3Plugin : TweenPlugin<Vector3, Vector3>
    {
        public override void SetValues(Tweener<Vector3, Vector3> tweener)
        {
            tweener.startValue = tweener.getter();
            tweener.changeValue = tweener.endValue - tweener.startValue;
        }
        
        public override void EvaluateAndApply(
            Tweener<Vector3, Vector3> tweener, DOSetter<Vector3> setter, 
            float elapsed, float duration, 
            Vector3 startValue, Vector3 changeValue
        )
        {
            float easeVal = EaseManager.Evaluate(tweener.easeType, elapsed, duration);
            setter(startValue + changeValue * easeVal);
        }
    }
}