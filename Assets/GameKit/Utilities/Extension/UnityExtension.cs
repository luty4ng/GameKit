using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;
using System.Collections.Generic;
namespace GameKit
{

    public static partial class UnityExtension
    {
        private static List<Transform> s_CachedTransforms;
        public static Vector2 ToLocal(this Vector2 position, Transform transform)
        {
            return transform.InverseTransformPoint(position);
        }

        public static Vector3 ToLocal(this Vector3 position, Transform transform)
        {
            return transform.InverseTransformPoint(position);
        }

        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.GetComponentsInChildren(true, s_CachedTransforms);
            for (int i = 0; i < s_CachedTransforms.Count; i++)
            {
                s_CachedTransforms[i].gameObject.layer = layer;
            }

            s_CachedTransforms.Clear();
        }

        public static bool InScene(this GameObject gameObject)
        {
            return gameObject.scene.name != null;
        }

        public static void RemoveComponent<Component>(this GameObject obj, bool immediate = false) {
            Component component = obj.GetComponent<Component>();
            if (component != null) {
                if (immediate) {
                    UnityEngine.Object.DestroyImmediate(component as UnityEngine.Object, true);
                } else {
                    UnityEngine.Object.Destroy(component as UnityEngine.Object);
                }
            }
        }

        public static void LocalRest(this Transform tr) {
            tr.localPosition = Vector3.zero;
            tr.localRotation = Quaternion.identity;
            tr.localScale = Vector3.one;
        }
        public static void Reset(this Transform tr) {
            tr.position = Vector3.zero;
            tr.rotation = Quaternion.identity;
            tr.localScale = Vector3.zero;
        }
        public static Transform FindParent(this Transform tr, string name) {
            Transform parent = tr.parent;
            while (parent != null && parent.name != name)
                parent = parent.parent;
            return parent;
        }
        public static T GetGetComponentInAllParents<T>(this GameObject go) where T:MonoBehaviour{
            GameObject parent = go.transform.parent.gameObject;
            while (parent != null ) {
                T output = parent.GetComponent<T>();
                if (output != null) return output;
            }
            return null;
        }

        /// <summary>
        /// 只设置颜色不改变alpha
        /// </summary>
        public static void SetColor(this Image image, Color color) {
            Color tempColor = color;
            tempColor.a = image.color.a;
            image.color = tempColor;
        }

        /// <summary>
        /// 只设置alpha不改变颜色
        /// </summary>
        public static void SetAlpha(this Image image, float alpha) {
            Color tempColor = image.color;
            tempColor.a = alpha;
            image.color = tempColor;
        }

        /// <summary>
        /// 只设置alpha不改变颜色
        /// </summary>
        public static void SetAlpha(this SpriteRenderer spriteRenderer, float alpha) {
            Color tempColor = spriteRenderer.color;
            tempColor.a = alpha;
            spriteRenderer.color = tempColor;
        }

        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action) {
            if (action == null || dictionary.Count == 0) return;
            for (int i = 0; i < dictionary.Count; i++) {
                var item = dictionary.ElementAt(i);
                action(item);
            }
        }
    }
}
