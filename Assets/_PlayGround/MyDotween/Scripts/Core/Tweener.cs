using System.Collections.Generic;
using System;

namespace MyDOTween
{
    public class Tweener<TweenVT, StoreVT> : Tween
    {
        internal bool start;
        public DOGetter<TweenVT> getter = null;
        public DOSetter<TweenVT> setter = null;
        internal TweenPlugin<TweenVT, StoreVT> tweenPlugin;

        public StoreVT startValue;
        public StoreVT endValue;
        public StoreVT changeValue;

        public float elapsed;
        public float duration;

        internal Ease easeType;

        internal Tweener()
        {
            tweenType = TweenType.Tweener;
        }

        public bool Setup(DOGetter<TweenVT> getter, DOSetter<TweenVT> setter, StoreVT endValue, float duration)
        {
            this.start = false;
            this.getter = getter;
            this.setter = setter;

            if (tweenPlugin == null)
            {
                tweenPlugin = PluginManager.GetDefaultPlugin<TweenVT, StoreVT>();
            }
            this.endValue = endValue;
            this.elapsed = 0f;
            this.duration = duration;
            this.easeType = DOTween.defaultEaseType;
            return true;
        }

        internal override void Update(float deltaTime)
        {
            if (!active)
                return;

            if (!start)
            {
                tweenPlugin.SetValues(this);
                start = true;
            }

            elapsed = Math.Min(elapsed + deltaTime, duration);
            if (elapsed == duration)
            {
                active = false;
            }
            tweenPlugin.EvaluateAndApply(this, setter, elapsed, duration, startValue, changeValue);
        }
    }
}