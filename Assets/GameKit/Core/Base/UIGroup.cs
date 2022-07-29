using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIGroup : UIBehaviour
    {
        private Dictionary<string, List<UIBehaviour>> uiComponet = new Dictionary<string, List<UIBehaviour>>();
        protected CanvasGroup panelCanvasGroup;
        protected override void Awake()
        {
            UIManager.instance.RegisterUI(this as UIGroup);
            FindChildrenByType<Button>();
            FindChildrenByType<Image>();
            FindChildrenByType<Text>();
            FindChildrenByType<Toggle>();
            FindChildrenByType<Slider>();
            FindChildrenByType<UIForm>();
            FindChildrenByType<LayoutGroup>();
        }
        protected override void Start()
        {
            panelCanvasGroup = GetComponent<CanvasGroup>();
            OnStart();
        }

        protected virtual void OnStart() { }
        public virtual void Show(UnityAction callback = null) { }
        public virtual void Hide(UnityAction callback = null) { }
        public T GetUIComponent<T>(string name) where T : UIBehaviour
        {
            if (uiComponet.ContainsKey(name))
            {
                for (int i = 0; i < uiComponet[name].Count; ++i)
                {
                    if (uiComponet[name][i] is T)
                    {
                        return uiComponet[name][i] as T;
                    }
                }
            }
            return null;
        }

        protected void FindChildrenByType<T>() where T : UIBehaviour
        {
            T[] components = this.GetComponentsInChildren<T>(true);
            for (int i = 0; i < components.Length; ++i)
            {
                if (components[i].transform.parent != this)
                    continue;

                if (components[i].GetType() == typeof(UIForm))
                {
                    (components[i] as UIForm).Group = this;
                }

                string objName = components[i].gameObject.name;
                if (uiComponet.ContainsKey(objName))
                    uiComponet[objName].Add(components[i]);
                else
                    uiComponet.Add(objName, new List<UIBehaviour>() { components[i] });
            }
        }

        protected override void OnDestroy()
        {
            UIManager.instance.RemoveUI(this);
        }
    }

}

