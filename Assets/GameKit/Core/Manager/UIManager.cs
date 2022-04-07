using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace GameKit
{
    public class UIManager : SingletonBase<UIManager>
    {

        private Dictionary<string, UIGroup> panels = new Dictionary<string, UIGroup>();
        public void RegisterUI(UIGroup panel)
        {
            if (panels == null)
                panels = new Dictionary<string, UIGroup>();

            if (!panels.ContainsKey(panel.gameObject.name))
                panels.Add(panel.gameObject.name, panel);
            else
                panels[panel.gameObject.name] = panel;
        }

        public void RemoveUI(UIGroup panel)
        {
            if (panels.ContainsKey(panel.gameObject.name))
                panels.Remove(panel.gameObject.name);
        }

        public void Clear()
        {
            if (panels == null)
                return;
            if (panels.Count > 0)
                panels.Clear();
        }
        
        public void ShowUI(string uiName, UnityAction callback = null)
        {
            if (panels.ContainsKey(uiName))
                panels[uiName].Show();
            callback?.Invoke();
        }

        public void ShowUI<T>(string uiName, UnityAction callback = null) where T : UIGroup
        {
            if (panels.ContainsKey(uiName))
                (panels[uiName] as T).Show();
            callback?.Invoke();
        }

        public void HideUI(string uiName, UnityAction callback = null)
        {
            if (panels.ContainsKey(uiName))
                panels[uiName].Hide();
            callback?.Invoke();
        }

        public void HideUI<T>(string uiName, UnityAction callback = null) where T : UIGroup
        {
            if (panels.ContainsKey(uiName))
                (panels[uiName] as T).Hide();
            callback?.Invoke();
        }

        public UIGroup GetUI(string name)
        {
            if (panels.ContainsKey(name))
                return panels[name];
            return null;
        }
        
        public T GetUI<T>(string name) where T : UIGroup
        {
            if (panels.ContainsKey(name))
                return panels[name] as T;
            return null;
        }
    }
}

