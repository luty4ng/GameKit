using System.Collections.Generic;

namespace MyDOTween {
    internal static class TweenManager {
        // active tweens
        internal static List<Tween> activeTweens = new List<Tween>();

        // get a new tweener
        internal static Tweener<TweenVT, StoreVT> GetTweener<TweenVT, StoreVT>() {
            Tweener<TweenVT, StoreVT> tweener = new Tweener<TweenVT, StoreVT>();
            AddActiveTween(tweener);
            return tweener;
        }

        // get a new sequence
        internal static Sequence GetSequence() {
            Sequence sequence = new Sequence();
            AddActiveTween(sequence);
            return sequence;
        }

        // whether there're active tweens
        internal static bool hasActiveTweens() {
            return activeTweens.Count > 0;
        }

        private static void AddActiveTween(Tween tween) {
            // set active
            tween.active = true;
            // add to active list
            activeTweens.Add(tween);
        }

        internal static void AddActiveTweenToSequence(Tween tween) {
            RemoveActiveTween(tween);
        }

        private static void RemoveActiveTween(Tween tween) {
            // remove from active list
            activeTweens.Remove(tween);
        }

        internal static void Update(float deltaTime) {
            // if some of the tweens are inactive, they need to be killed
            bool willKill = false;
            foreach (Tween tween in activeTweens) {
                if (!tween.active) {
                    willKill = true;
                }
                else {
                    tween.Update(deltaTime);
                    if (!tween.active) {
                        willKill = true;
                    }
                }
            }
            if (willKill) {
                activeTweens.RemoveAll(t => !t.active);
            }
        }
    }
}