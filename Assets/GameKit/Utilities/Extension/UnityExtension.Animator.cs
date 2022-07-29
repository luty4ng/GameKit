using UnityEngine;
using UnityEngine.Events;

namespace GameKit
{
    public static partial class UnityExtension
    {
        private static Animator s_animator;
        private static UnityAction onFinish;
        private static float normalizedTime;
        public static void OnComplete(this Animator animator, float checkTime = 0.8f, UnityAction callback = null)
        {
            s_animator = animator;
            onFinish = callback;
            normalizedTime = checkTime;
            MonoManager.instance.AddUpdateListener(CheckAnimatorComplete);
        }

        private static void CheckAnimatorComplete()
        {
            AnimatorStateInfo info = s_animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= normalizedTime)
            {
                onFinish?.Invoke();
                MonoManager.instance.RemoveUpdateListener(CheckAnimatorComplete);
            }
        }
    }
}
