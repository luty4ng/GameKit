using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameKit
{
    [RequireComponent(typeof(Button)), RequireComponent(typeof(Animator))]
    public class Button_Anim : UIForm, IPointerEnterHandler, IPointerExitHandler
    {
        public enum AnimType
        {
            Scale,
            None
        }
        public AnimType animType;
        private Animator animator;

        protected override void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            if (animType == AnimType.Scale)
                animator.SetTrigger("ScaleUp");
        }

        public void OnPointerExit(PointerEventData pointerEventData)
        {
            if (animType == AnimType.Scale)
                animator.SetTrigger("ScaleDown");
        }
    }
}
