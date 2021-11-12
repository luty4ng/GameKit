using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BasePanel : UIBehaviour
{
    private Dictionary<string, List<UIBehaviour>> panel = new Dictionary<string, List<UIBehaviour>>();
    protected override void Start()
    {
        FindChildrenByType<Button>();
        FindChildrenByType<Image>();
        FindChildrenByType<Text>();
        FindChildrenByType<Toggle>();
        FindChildrenByType<Slider>();
        FindChildrenByType<LayoutGroup>();
        FindChildrenByType<BasePanel>();
        OnStart();
    }

    protected virtual void OnStart()
    {
        foreach (var item in panel)
        {
            Debug.Log(this.gameObject.name + "-" + item.Value.GetType());
        }
    }

    public virtual void Show()
    {

    }

    public virtual void Hide()
    {

    }

    public T GetComponentInDic<T>(string name) where T : UIBehaviour
    {
        if (panel.ContainsKey(name))
        {
            for (int i = 0; i < panel[name].Count; ++i)
            {
                if (panel[name][i] is T)
                {
                    return panel[name][i] as T;
                }
            }
        }
        return null;
    }

    private void FindChildrenByType<T>() where T : UIBehaviour
    {
        T[] components = this.GetComponentsInChildren<T>();
        for (int i = 0; i < components.Length; ++i)
        {
            if (components[i].transform.parent == this.gameObject.transform)
            {
                string objName = components[i].gameObject.name;
                if (panel.ContainsKey(objName))
                    panel[objName].Add(components[i]);
                else
                    panel.Add(objName, new List<UIBehaviour>() { components[i] });
            }
        }
    }
}
