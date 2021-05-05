using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 里式转化原则  基类可以转化为子类  声明使用积累， 方法使用子类， 可以起到局部方法使用泛型的特点
// 利用泛型给委托传参
public interface IEventInfo { }
public class EventInfo<T> : IEventInfo
{
    public UnityAction<T> actions;
    public EventInfo(UnityAction<T> action)
    {
        actions += action;
    }
}

public class EventInfo : IEventInfo
{
    public UnityAction actions;
    public EventInfo(UnityAction action)
    {
        actions += action;
    }
}

public class EventCenter : BaseManager<EventCenter>
{

    private Dictionary<string, IEventInfo> events = new Dictionary<string, IEventInfo>();
    public void AddEventListener<T>(string name, UnityAction<T> action)
    {
        if(events.ContainsKey(name))
        {
            (events[name] as EventInfo<T>).actions += action;
        }
        else
        {
            events.Add(name, new EventInfo<T>(action));
        }
    }
    
    public void AddEventListener(string name, UnityAction action)
    {
        if(events.ContainsKey(name))
        {
            (events[name] as EventInfo).actions += action;
        }
        else
        {
            events.Add(name, new EventInfo(action));
        }
    }

    
    public void EventTrigger<T>(string name, T info)
    {
        // 在这个函数里可以处理传参信息 info
        if(events.ContainsKey(name))
        {
            if((events[name] as EventInfo<T>).actions != null)
            {
                (events[name] as EventInfo<T>).actions.Invoke(info);
            }
        }
    }

    public void EventTrigger(string name)
    {
        // 在这个函数里可以处理传参信息 info
        if(events.ContainsKey(name))
        {
            if((events[name] as EventInfo).actions != null)
            {
                (events[name] as EventInfo).actions.Invoke();
            }
        }
    }

    public void RemoveEventListener<T>(string name, UnityAction<T> action)
    {
        if(events.ContainsKey(name))
        {
            (events[name] as EventInfo<T>).actions -= action;
        }
    }

    public void RemoveEventListener(string name, UnityAction action)
    {
        if(events.ContainsKey(name))
        {
            (events[name] as EventInfo).actions -= action;
        }
    }
    public void Clear()
    {
        events.Clear();
    }
}