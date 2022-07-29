using System.Collections.Generic;
using UnityEngine.Events;
using GameKit;
using GameKit.DataStructure;

[System.Serializable]
public class Option
{
    public int autoIncreaseIndex;
    public bool multiParse;
    public string text;
    public UnityAction callback;

    public Option(int index, string text, bool multiParse = false, UnityAction callback = null)
    {
        this.autoIncreaseIndex = index;
        this.text = text;
        this.multiParse = multiParse;
        this.callback = callback;
    }

    public void BeSelected()
    {
        callback?.Invoke();
    }
}

public class DialogSelection
{
    private static List<Option> options;
    private static int currentIndex = 0;

    public static void CreateOption(int index, string text, bool multiParse = false, UnityAction callback = null)
    {
        Option newOption = new Option(index, text, multiParse, callback);
        options.Add(newOption);
    }

    public static List<Option> CreateSelection(List<INode> nodes)
    {
        options = new List<Option>();
        for (int i = 0; i < nodes.Count; i++)
        {
            Node<Dialog> dialogNode = nodes[i] as Node<Dialog>;
            string contents = dialogNode.nodeEntity.contents;
            CreateOption(i, contents);
        }
        return options;
    }
}