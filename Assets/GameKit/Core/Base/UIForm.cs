using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit
{
    public class UIForm : UIBehaviour
    {
        [HideInInspector] public RectTransform rectTransform;
        protected UIGroup group;
        public UIGroup Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        protected override void Start()
        {

            OnStart();
        }
        public virtual void OnTick() { }
        public virtual void OnStart() { }
    }
}

