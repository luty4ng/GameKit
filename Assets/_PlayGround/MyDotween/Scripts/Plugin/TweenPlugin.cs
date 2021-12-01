namespace MyDOTween
{
    public abstract class TweenPlugin<TweenVT, StoreVT> : ITweenPlugin
    {
        public abstract void SetValues(Tweener<TweenVT, StoreVT> tweener);
        public abstract void EvaluateAndApply(
            Tweener<TweenVT, StoreVT> tweener, DOSetter<TweenVT> setter,
            float elapsed, float duration,
            StoreVT startValue, StoreVT changeValue
        );
    }
}