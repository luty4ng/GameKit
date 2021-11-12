using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;


public enum UILayer
{
    bot,
    mid,
    top,
    system
}
public class UIManager : BaseManager<UIManager>
{
    private Transform bot;
    private Transform mid;
    private Transform top;
    private Transform system;

    public Dictionary<string, BasePanel> panel = new Dictionary<string, BasePanel>();

    public UIManager()
    {
        GameObject obj = ResourceManager.instance.Load<GameObject>("UI/Canvas");
        GameObject.DontDestroyOnLoad(obj);
        Transform canvas = obj.transform;
        obj = ResourceManager.instance.Load<GameObject>("UI/EventSystem");
        GameObject.DontDestroyOnLoad(obj);

        bot = canvas.Find("Bot");
        mid = canvas.Find("Mid");
        top = canvas.Find("Top");
        system = canvas.Find("System");
    }
    public void ShowPanel<T>(string panelName, UILayer layer = UILayer.system, UnityAction<T> callback = null) where T : BasePanel
    {
        if (panel.ContainsKey(panelName))
            callback?.Invoke(panel[panelName] as T);

        ResourceManager.instance.LoadAsync<GameObject>("UI/" + panelName, (obj) =>
        {
            Transform temp = bot;
            switch (layer)
            {
                case UILayer.mid:
                    temp = mid;
                    break;
                case UILayer.top:
                    temp = top;
                    break;
                case UILayer.system:
                    temp = system;
                    break;
            }

            obj.transform.SetParent(temp);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            (obj.transform as RectTransform).offsetMax = Vector2.zero;
            (obj.transform as RectTransform).offsetMin = Vector2.zero;

            T panelComp = obj.GetComponent<T>();
            callback?.Invoke(panelComp);
            panel.Add(panelName, panelComp);
        });
    }

    public void HidePanel(string panelName)
    {
        if (panel.ContainsKey(panelName))
        {
            GameObject.Destroy(panel[panelName].gameObject);
            panel.Remove(panelName);
        }
    }

}