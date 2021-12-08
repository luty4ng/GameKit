using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class LayoutPreview : MonoBehaviour
{
    public enum LayoutType
    {
        Queue,
        Square
    }
    public bool isRuntime = false;
    public float spacing = 10f;
    public LayoutType layoutType = LayoutType.Square;
    public void Rearrange()
    {
        int childCount = this.transform.childCount;
        Debug.Log(childCount);
        List<Transform> children = new List<Transform>(this.GetComponentsInChildren<Transform>());
        List<Transform> delete = (from child in children where child.transform.parent!=this.transform select child).ToList();
        // if (children.Contains(this.transform))
        //     children.Remove(this.transform);
        foreach (var child in delete)
            children.Remove(child);
        delete.Clear();
                
        

        if (layoutType == LayoutType.Square)
        {
            int width = (int)Mathf.Ceil(Mathf.Sqrt(childCount));
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if ((i * width) + j > childCount - 1)
                    {
                        Debug.Log((i * width) + j);
                        break;
                    }

                    children[(i * width) + j].position = this.transform.position + new Vector3(i, 0, j) * spacing;
                }
            }
        }
        else if (layoutType == LayoutType.Queue)
        {
            for (int i = 0; i < childCount - 1; i++)
            {
                children[i].position = this.transform.position + new Vector3(i, 0, 0) * spacing;
            }
        }
    }

    private void Update()
    {
        if (isRuntime)
            Rearrange();
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(LayoutPreview))]
public class LayoutPreviewEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LayoutPreview layoutPreview = (LayoutPreview)target;
        if (GUILayout.Button("Rearrange"))
        {
            layoutPreview.Rearrange();
        }
    }
}
#endif