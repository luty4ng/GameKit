using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

namespace GameKit
{
    [System.Serializable]
    public class FirstEnterLevelData
    {
        public List<int> visitedLevel;

        public FirstEnterLevelData()
        {
            visitedLevel = new List<int>();
        }
    }

    public class BootManager : MonoBehaviour
    {
        public static BootManager instance;
        private string mainMenu = "MainMenu";
        public string currentScene = "MainMenu";
        public string centerScene = "LevelRoom";
        private FirstEnterLevelData firstEnterLevelData;
        private void Awake()
        {
            if (instance == null)
                instance = this;
            if (SceneManager.sceneCount > 1)
            {
                currentScene = SceneManager.GetSceneAt(1).name;
            }
            else
            {
                LoadSceneAsyn(mainMenu);
                currentScene = "MainMenu";
            }

            if (JsonManager.instance.CheckJsonExist("SerializedData/FirstEnterLevelData"))
                firstEnterLevelData = JsonManager.instance.LoadJsonData<FirstEnterLevelData>("SerializedData/FirstEnterLevelData");
            else
                firstEnterLevelData = new FirstEnterLevelData();

        }

        public void LoadSceneWithTransition(string name, LoadSceneMode mode = LoadSceneMode.Additive, UnityAction callback = null)
        {
            // (UIManager.instance.panels["UI_transition"] as TransitionPanelUI).StartTransition(name);
        }

        public void LoadSceneAsyn(string name, LoadSceneMode mode = LoadSceneMode.Additive, UnityAction callback = null)
        {
            StartCoroutine(LoadSceneAsynIE(name, mode, callback));
        }
        public void UnloadSceneAsyn(string name, UnityAction callback)
        {
            StartCoroutine(UnloadSceneAsyncIE(name, callback));
        }

        IEnumerator LoadSceneAsynIE(string name, LoadSceneMode mode, UnityAction callback)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name, mode);
            while (!ao.isDone)
            {
                // EventCenter.instance.EventTrigger("Loading Scene", ao.progress);
                yield return ao.progress;
            }
            callback?.Invoke();
        }

        IEnumerator UnloadSceneAsyncIE(string name, UnityAction callback)
        {
            AsyncOperation ao = SceneManager.UnloadSceneAsync(name);
            while (!ao.isDone)
            {
                // EventCenter.instance.EventTrigger("Removing Scene", ao.progress);
                yield return ao.progress;
            }
            callback?.Invoke();
        }
        // return 0 means not an availiable level.
        public int GetActiveSceneNumber()
        {
            if (SceneManager.sceneCount > 1)
            {
                int order = 0;
                string[] levelSplit = SceneManager.GetSceneAt(1).name.Split('_');
                if (levelSplit.Length <= 1)
                    return 0;
                System.Int32.TryParse(levelSplit[1], out order);
                return order;
            }
            Debug.LogWarning("No Active Level in Scene");
            return 0;
        }

        public bool CheckFirstEnter(int level)
        {
            if (firstEnterLevelData == null)
                return false;
            if (!firstEnterLevelData.visitedLevel.Contains(level))
            {
                firstEnterLevelData.visitedLevel.Add(level);
                return true;
            }
            return false;
        }

        private void OnDestroy()
        {
            JsonManager.instance.SaveJsonData<FirstEnterLevelData>("SerializedData/FirstEnterLevelData", firstEnterLevelData);
        }
    }

}

