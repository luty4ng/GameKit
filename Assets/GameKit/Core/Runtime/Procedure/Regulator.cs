using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace GameKit
{
    public class Regulator<T> : MonoBehaviour where T : Regulator<T>
    {
        private Dictionary<string, IContainer> containers;
        public static T current;
        private void Awake()
        {
            current = this as T;
        }

        public void Quit() => Application.Quit();
        public UIGroup GetUI(string name) => UIManager.instance.GetUI(name);
        public void ShowUI(string name) => GetUI(name).Show();
        public void HideUI(string name) => GetUI(name).Hide();
        public void SwitchSceneSwipe(string name) => Scheduler.instance.SwitchSceneSwipe(name);
        public void SwitchScene(string name) => Scheduler.instance.SwitchScene(name);
        public void ReloadCurrentSceneSwipe() => Scheduler.instance.ReloadCurrentSceneSwipe();
        public void RegisterContainer<T1>(T1 container) where T1 : IContainer
        {
            string name = container.GetType().Name;
            if (!containers.ContainsKey(name))
                containers.Add(name, container);
        }
        public T1 GetContainer<T1>(string name) where T1 : class
        {
            if (containers.ContainsKey(name))
                return (containers[name] as T1);
            return default(T1);
        }
        public IContainer GetContainer(string name)
        {
            if (containers.ContainsKey(name))
                return containers[name];
            return null;
        }
        protected IEnumerator DelayedExcute(UnityAction action, float t)
        {
            yield return new WaitForSeconds(t);
            action?.Invoke();
        }
    }
}