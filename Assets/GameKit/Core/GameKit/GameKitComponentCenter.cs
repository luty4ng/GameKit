using UnityEngine;
using System;
using System.Collections.Generic;
using GameKit;


namespace GameKit
{
    public static class GameKitComponentCenter
    {
        private static readonly CachedLinkedList<GameKitComponent> s_GameKitComponents = new CachedLinkedList<GameKitComponent>();
        internal const int GameKitSceneId = 0;

        public static T GetComponent<T>() where T : GameKitComponent
        {
            return (T)GetComponent(typeof(T));
        }

        public static GameKitComponent GetComponent(Type type)
        {
            LinkedListNode<GameKitComponent> current = s_GameKitComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }

                current = current.Next;
            }
            return null;
        }

        public static GameKitComponent GetComponent(string typeName)
        {
            LinkedListNode<GameKitComponent> current = s_GameKitComponents.First;
            while (current != null)
            {
                Type type = current.Value.GetType();
                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        internal static void RegisterComponent(GameKitComponent gameKitComponent)
        {
            if (gameKitComponent == null)
            {
                Debug.LogError("Game Kit component is invalid.");
                return;
            }

            Type ctype = gameKitComponent.GetType();

            LinkedListNode<GameKitComponent> current = s_GameKitComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == ctype)
                {
                    Debug.LogError("Game Kit component type is already exist: " + ctype.FullName);
                    return;
                }
                current = current.Next;
            }

            s_GameKitComponents.AddLast(gameKitComponent);
        }
    }


    //         public static void Shutdown(ShutdownType shutdownType)
    //         {
    //             Log.Info("Shutdown Game Framework ({0})...", shutdownType.ToString());
    //             BaseComponent baseComponent = GetComponent<BaseComponent>();
    //             if (baseComponent != null)
    //             {
    //                 baseComponent.Shutdown();
    //                 baseComponent = null;
    //             }

    //             s_GameFrameworkComponents.Clear();

    //             if (shutdownType == ShutdownType.None)
    //             {
    //                 return;
    //             }

    //             if (shutdownType == ShutdownType.Restart)
    //             {
    //                 SceneManager.LoadScene(GameFrameworkSceneId);
    //                 return;
    //             }

    //             if (shutdownType == ShutdownType.Quit)
    //             {
    //                 Application.Quit();
    // #if UNITY_EDITOR
    //                 UnityEditor.EditorApplication.isPlaying = false;
    // #endif
    //                 return;
    //             }
    //         }
}

