using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit;

namespace GameKit
{
    public interface IEventInfo { }
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;
        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
        public void Clear()
        {
            System.Delegate[] acts = actions.GetInvocationList();
            for (int i = 0; i < acts.Length; i++)
            {
                actions -= acts[i] as UnityAction<T>;
            }
        }
    }

    public class EventInfo<T0, T1> : IEventInfo
    {
        public UnityAction<T0, T1> actions;
        public EventInfo(UnityAction<T0, T1> action)
        {
            actions += action;
        }
        public void Clear()
        {
            System.Delegate[] acts = actions.GetInvocationList();
            for (int i = 0; i < acts.Length; i++)
            {
                actions -= acts[i] as UnityAction<T0, T1>;
            }
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;
        public EventInfo(UnityAction action)
        {
            actions += action;
        }
        public void Clear()
        {
            System.Delegate[] acts = actions.GetInvocationList();
            for (int i = 0; i < acts.Length; i++)
            {
                actions -= acts[i] as UnityAction;
            }
        }
    }
    public class EventManager : SingletonBase<EventManager>
    {

        private Dictionary<string, IEventInfo> events = new Dictionary<string, IEventInfo>();
        public void AddEventListener<T>(string name, UnityAction<T> action)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo<T>).actions += action;
            }
            else
            {
                events.Add(name, new EventInfo<T>(action));
            }
        }

        public void AddEventListener<T0, T1>(string name, UnityAction<T0, T1> action)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo<T0, T1>).actions += action;
            }
            else
            {
                events.Add(name, new EventInfo<T0, T1>(action));
            }
        }

        public void AddEventListener(string name, UnityAction action)
        {
            if (events.ContainsKey(name))
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
            if (events.ContainsKey(name))
            {
                if ((events[name] as EventInfo<T>).actions != null)
                {
                    (events[name] as EventInfo<T>).actions?.Invoke(info);
                }
            }
        }

        public void EventTrigger<T0, T1>(string name, T0 info1, T1 info2)
        {
            if (events.ContainsKey(name))
            {
                if ((events[name] as EventInfo<T0, T1>).actions != null)
                {
                    (events[name] as EventInfo<T0, T1>).actions?.Invoke(info1, info2);
                }
            }
        }

        public void EventTrigger(string name)
        {
            if (events.ContainsKey(name))
            {
                if ((events[name] as EventInfo).actions != null)
                {
                    (events[name] as EventInfo).actions.Invoke();
                }
            }
        }

        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo<T>).actions -= action;
            }
        }

        public void RemoveEventListener<T0, T1>(string name, UnityAction<T0, T1> action)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo<T0, T1>).actions -= action;
            }
        }

        public void RemoveEventListener(string name, UnityAction action)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo).actions -= action;
            }
        }

        public void ClearEventListener(string name)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo).Clear();
            }
        }

        public void ClearEventListener<T>(string name)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo<T>).Clear();
            }
        }

        public void ClearEventListener<T0, T1>(string name)
        {
            if (events.ContainsKey(name))
            {
                (events[name] as EventInfo<T0, T1>).Clear();
            }
        }
        public void Clear()
        {
            events.Clear();
        }

        public Dictionary<string, IEventInfo> GetEvents()
        {
            return events;
        }
    }
}