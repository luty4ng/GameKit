using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using DG.Tweening;
namespace GameKit
{
    public enum SceneSwitchType
    {
        Swipe,
        Fade,
        Animation,
        LoadingScene,
        Immediately
    }
    public delegate void SceneAction();
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Scheduler")]
    public class Scheduler : MonoSingletonBase<Scheduler>
    {
        public SceneSwitchType defaultSwitchType = SceneSwitchType.Swipe;
        public string StartScene = "GameKit_Main";
        public string LoadingScene = "GameKit_Loading";
        private Switcher switcher;
        public string CurrentScene
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            if (SceneManager.sceneCount > 1)
                CurrentScene = SceneManager.GetSceneAt(1).name;
            else
                LoadSceneAsyn(StartScene, callback: () => { CurrentScene = StartScene; });
            switcher = GetComponentInChildren<Switcher>();
        }
        public void SwitchSceneByDefault(string name)
        {
            if (defaultSwitchType == SceneSwitchType.Swipe)
                SwitchSceneBySwipe(name);
            else if (defaultSwitchType == SceneSwitchType.LoadingScene)
                SwitchSceneByLoadingScene(name);
            else if (defaultSwitchType == SceneSwitchType.Fade)
                SwitchSceneByFade(name);
            else if (defaultSwitchType == SceneSwitchType.Animation)
                SwitchSceneByAnimation(name);
            else if (defaultSwitchType == SceneSwitchType.Immediately)
                SwitchScene(name);
        }
        public void SwitchSceneByAnimation(string name)
        {
            switcher.animator.gameObject.SetActive(true);
            switcher.animator.SetTrigger("Swicth");
            switcher.animator.OnComplete(1f, () =>
            {
                UnloadSceneAsyn(CurrentScene, () =>
                {
                    LoadSceneAsyn(name, () =>
                    {
                        CurrentScene = name;
                        switcher.animator.SetTrigger("UnSwicth");
                        switcher.animator.OnComplete(1f, () =>
                        {
                            switcher.animator.gameObject.SetActive(false);
                        });
                    });
                });
            });
        }

        public void SwitchSceneBySwipe(string name)
        {
            switcher.swiper.gameObject.SetActive(true);
            switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
            {
                UnloadSceneAsyn(CurrentScene, () =>
                {
                    LoadSceneAsyn(name, () =>
                    {
                        CurrentScene = name;
                        switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(() =>
                        {
                            switcher.swiper.localPosition = new Vector3(2420f, switcher.swiper.localPosition.y, switcher.swiper.localPosition.z);
                            switcher.swiper.gameObject.SetActive(false);
                        });
                    });
                });
            });
        }

        public void SwitchSceneByFade(string name)
        {
            switcher.gradienter.gameObject.SetActive(true);
            switcher.gradienter.DOFade(1, 0.5f).OnComplete(() =>
            {
                UnloadSceneAsyn(CurrentScene, () =>
                {
                    LoadSceneAsyn(name, () =>
                    {
                        CurrentScene = name;
                        switcher.gradienter.DOFade(0f, 0.5f).OnComplete(() =>
                        {
                            switcher.gradienter.gameObject.SetActive(false);
                        });
                    });
                });
            });
        }

        public void SwitchScene(string name, UnityAction callback = null)
        {
            UnloadSceneAsyn(CurrentScene, () =>
            {
                LoadSceneAsyn(name, () =>
                {
                    CurrentScene = name;
                });
            });
        }

        public void ReloadCurrentSceneSwipe()
        {
            switcher.swiper.gameObject.SetActive(true);
            switcher.swiper.DOLocalMoveX(0, 0.5f).OnComplete(() =>
            {
                UnloadSceneAsyn(CurrentScene, () =>
                {
                    LoadSceneAsyn(CurrentScene, () =>
                    {
                        switcher.swiper.DOLocalMoveX(-2420f, 0.5f).OnComplete(() =>
                        {
                            switcher.swiper.localPosition = new Vector3(2420f, switcher.swiper.localPosition.y, switcher.swiper.localPosition.z);
                            switcher.swiper.gameObject.SetActive(false);
                        });
                    });
                });
            });
        }

        public void SwitchSceneByLoadingScene(string name, UnityAction callback = null)
        {
            ScenesManager.instance.TryGetScene(name, out Scene scene);
            if (scene == null)
            {
                Debug.LogError("No such scene in build settings.");
                return;
            }
            SwitchScene(LoadingScene, callback);
        }

        private void LoadSceneAsyn(string name, UnityAction callback = null)
        {
            ScenesManager.instance.LoadSceneAsynAdd(name, callback);
        }
        private void LoadSceneAsynSingle(string name, UnityAction callback = null)
        {
            ScenesManager.instance.LoadSceneAsyn(name, callback);
        }
        private void UnloadSceneAsyn(string name, UnityAction callback = null)
        {
            ScenesManager.instance.UnloadSceneAsyn(name, callback);
        }

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
    }
}