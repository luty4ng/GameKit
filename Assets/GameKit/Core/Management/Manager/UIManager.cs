using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace GameKit
{
    public class UIManager : BaseManager<UIManager>
    {
        public Dictionary<string, BasePanelUI> panels = new Dictionary<string, BasePanelUI>();
        public void ShowPanel<T>(string panelName, UnityAction<T> callback = null) where T : BasePanelUI
        {

        }

        public void HidePanel(string panelName)
        {

        }

        public void RegisterUI(BasePanelUI panel)
        {
            if (panels == null)
                panels = new Dictionary<string, BasePanelUI>();
            panels.Add(panel.gameObject.name, panel);
        }

        public void RemoveUI(BasePanelUI panel)
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
    }
}

