using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameKit.DataStructure;
public delegate void NodeEvent<in T>(T obj);
public delegate void NodeEvent();
public enum SpritePos
{
    Left,
    Right
}
public sealed class Dialog : NodeType
{
    public Sprite sprite;
    public SpritePos pos;
    public string speaker;
    public string contents;
    public string moodName;
    public bool IsFunctional = false;
    public bool IsDivider = false;
    public bool IsCompleter = false;
    public List<string> completeConditons;
    public List<string> dividerConditions;

    public NodeEvent onEnter, onUpdate, onFinish, onWait, onExit;
    public Dialog()
    {
        this.speaker = "Default";
        this.contents = "Default";
        this.moodName = "Default";
        this.dividerConditions = new List<string>();
        this.completeConditons = new List<string>();
    }

    public Dialog(string speaker, string contents, string moodName = "Default")
    {
        this.speaker = speaker;
        this.contents = contents;
        this.moodName = moodName;
        this.dividerConditions = new List<string>();
        this.completeConditons = new List<string>();
    }

    public void ClearEvents()
    {
        onEnter = (NodeEvent)System.Delegate.RemoveAll(onEnter, onEnter);
    }

    public override void OnEnter()
    {
        onEnter?.Invoke();
    }

    public override void OnUpdate()
    {
        onUpdate?.Invoke();
    }
    public override void OnFinish()
    {
        onFinish?.Invoke();
    }

    public override string ToString()
    {
        return speaker + ": " + contents;
    }
}
