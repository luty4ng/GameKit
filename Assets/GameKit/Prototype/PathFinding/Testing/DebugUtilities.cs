using UnityEngine;

public static class DebugUtilities
{
    public static TextMesh GridVisual(string text, Vector3 localPosition, Color color, int fontSize = 10, TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 0)
    {
        string objectName = $"({localPosition.x.ToString()}, {localPosition.y.ToString()})";
        GameObject gameObject = new GameObject(objectName, typeof(TextMesh));
        Transform transform = gameObject.transform;
        GameObject debugger = GameObject.Find("Grid Debugger");
        if (debugger == null)
            debugger = new GameObject("Grid Debugger");
        transform.SetParent(debugger.transform, false);

        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

}