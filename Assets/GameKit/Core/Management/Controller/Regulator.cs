using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace GameKit
{
    public class Regulator : MonoBehaviour
    {
        public static Regulator current;
        private void Awake()
        {
            if (current == null)
                current = this;
        }

        private void OnDestroy()
        {
            current = null;
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void ShowPanel(string name)
        {
            UIManager.instance.GetPanel(name).Show();
        }

        public void HidePanel(string name)
        {
            UIManager.instance.GetPanel(name).Hide();
        }

        public void SwitchSceneSwipe(string name) => Scheduler.instance.SwitchSceneSwipe(name);
        public void SwitchScene(string name) => Scheduler.instance.SwitchScene(name);
    }
}