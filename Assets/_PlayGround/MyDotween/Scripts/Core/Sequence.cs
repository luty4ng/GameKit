using System.Collections.Generic;

namespace MyDOTween
{
    public class Sequence : Tween
    {
        internal readonly Queue<Tween> sequencedTweens = new Queue<Tween>();
        internal Sequence()
        {
            tweenType = TweenType.Sequence;
        }

        internal static Sequence DoInsert(Sequence sequence, Tween tween)
        {
            TweenManager.AddActiveTweenToSequence(tween);
            tween.isSequenced = true;
            sequence.sequencedTweens.Enqueue(tween);
            return sequence;
        }

        internal override void Update(float deltaTime)
        {
            if (!active)
                return;

            if (sequencedTweens.Count > 0)
            {
                Tween currentTween = sequencedTweens.Peek();
                currentTween.Update(deltaTime);
                if (!currentTween.active)
                {
                    sequencedTweens.Dequeue();
                }
            }
            active = (sequencedTweens.Count > 0);
        }
    }
}