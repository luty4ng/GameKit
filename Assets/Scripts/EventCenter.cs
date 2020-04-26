using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class EventCenter : BaseManager<EventCenter>
{
    // key: event name
    // value: delegrate of all events
    private Dictionary<string, UnityAction<object>> events = new Dictionary<string, UnityAction<object>>();

    // name: event name
    // action: delegrate that handle this event
    public void AddEventListener(string name, UnityAction<object> action)
    {
        if(events.ContainsKey(name))
        {
            events[name] += action;
        }
        else
        {
            events.Add(name, action);
        }
    }

    public void EventTrigger(string name, object info)
    {
        if(events.ContainsKey(name))
        {
            events[name].Invoke(info);
        }
    }

    public void RemoveEventListener(string name, UnityAction<object> action)
    {
        if(events.ContainsKey(name))
            events[name] -= action;
    }
    public void Clear()
    {
        events.Clear();
    }
}