using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BasePanel : MonoBehaviour
{
    private Dictionary<string, List<UIBehaviour>> panel = new  Dictionary<string, List<UIBehaviour>>();
    // Start is called before the first frame update
    void Awake()
    {
        FindChildrenByType<Button>();
        FindChildrenByType<Image>();
        FindChildrenByType<Text>();
        FindChildrenByType<Toggle>();
        FindChildrenByType<Slider>();
    }

    public virtual void Show()
    {
        // 显示面板时重写调用
    }

    public virtual void Hide()
    {
        // 隐藏面板时重写调用
    }

    // name: UI对象的名字       T： 得到的空间类别
    protected T GetComponentInDic<T>(string name) where T : UIBehaviour
    {
        if(panel.ContainsKey(name))
        {
            for (int i = 0; i < panel[name].Count; ++i)
            {
                if(panel[name][i] is T)
                {
                    return panel[name][i] as T;
                }
            }
        }
        return null;
    }

    // 一个name1的object下可能有多个type的空间
    private void FindChildrenByType<T>() where T : UIBehaviour
    {
        T[] components = this.GetComponentsInChildren<T>();
        string objName;
        for(int i = 0; i < components.Length; ++i)
        {
            objName = components[i].gameObject.name;
            if(panel.ContainsKey(objName))
                panel[objName].Add(components[i]);
            else
                panel.Add(objName, new List<UIBehaviour>() { components[i]} );
        }
    }
    
}
