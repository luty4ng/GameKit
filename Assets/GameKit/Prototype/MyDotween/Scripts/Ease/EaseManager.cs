using UnityEngine;
using System;

namespace MyDOTween
{
    public static class EaseManager
    {
        public static float Evaluate(Ease easeType, float elapsed, float duration)
        {
            switch (easeType)
            {
                case Ease.Linear:
                    return elapsed / duration;
                case Ease.InSine:
                    return -(float)Math.Cos((elapsed / duration) * (Mathf.PI * 0.5f)) + 1;
                default:
                    return 1;
            }
        }
    }
}