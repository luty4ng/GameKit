using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> panel = new Dictionary<string, List<UIBehaviour>>();

    void Start()
    {
        FindChildrenByType<Button>();
        FindChildrenByType<Image>();
        FindChildrenByType<Text>();
        FindChildrenByType<Toggle>();
        FindChildrenByType<Slider>();
    }

    public virtual void Show()
    {

    }

    public virtual void Hide()
    {

    }

    protected T GetComponentInDic<T>(string name) where T : UIBehaviour
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
        string objName;
        for (int i = 0; i < components.Length; ++i)
        {
            objName = components[i].gameObject.name;
            if (panel.ContainsKey(objName))
                panel[objName].Add(components[i]);
            else
                panel.Add(objName, new List<UIBehaviour>() { components[i] });
        }
    }
}
