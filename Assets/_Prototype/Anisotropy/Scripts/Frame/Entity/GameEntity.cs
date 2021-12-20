using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;
using System.Linq;
public enum TimeState
{
    Forward,
    Backward,
    Pause
}

public enum EntityLevel
{
    Ground,
    Block,
    Top
}

public abstract class GameEntity : MonoBehaviour, ITimeReactable
{
    public List<FrameData> frameData;
    public int currentFrame;
    public TimeState state = TimeState.Forward;
    public bool isControlAgent = false;
    protected bool timeReactable = true;
    protected virtual void OnStart() { }
    private void Start()
    {
        currentFrame = 0;
        frameData = new List<FrameData>();
        TimeManager.instance.AddEntity(this);
        EventManager.instance.AddEventListener(EventConfig.TimeChange.ToString(), () =>
        {
            if (timeReactable == false)
                return;
            StartCoroutine(TimeTrigger());
        });
        OnStart();
    }
    protected void SaveData()
    {

        FrameData data = new FrameData();
        data.position = this.transform.position;
        data.rotation = this.transform.rotation.eulerAngles;
        data.scale = this.transform.localScale;
        // Debug.Log(this.gameObject.name + " Save : " + data.position);
        frameData.Add(data);
        currentFrame += 1;
    }
    protected void UpdateData(int index)
    {
        if (index + 1 < frameData.Count)
        {
            FrameData data = new FrameData();
            data.position = this.transform.position;
            data.rotation = this.transform.rotation.eulerAngles;
            data.scale = this.transform.localScale;

            frameData[index] = data;
        }
    }
    protected void AssignData(FrameData status)
    {
        this.transform.position = status.position;
        this.transform.localScale = status.scale;
        this.transform.eulerAngles = status.rotation;
        // Debug.Log(this.gameObject.name + " Assign:" + status.position);
        if (status.commandSet == null)
            return;
        if (status.commandSet.hasCommand)
            status.commandSet.ExcuteCommand(state);

    }
    protected void LoadBackward()
    {
        currentFrame = currentFrame > 0 ? currentFrame - 1 : currentFrame;
        if (isControlAgent)
        {
            // OnUpdate();
            UpdateData(currentFrame);
            return;
        }
        AssignData(this.frameData[currentFrame]);
    }
    protected void CheckForward()
    {
        if (currentFrame == frameData.Count)
        {
            // OnUpdate();
            SaveData();
        }
        else if (currentFrame < frameData.Count)
        {
            if (isControlAgent)
            {
                // OnUpdate();
                if (currentFrame < frameData.Count && frameData.Count != 0)
                    frameData.RemoveRange(currentFrame, frameData.Count - 1);
                SaveData();
                return;
            }
            AssignData(this.frameData[currentFrame]);
            currentFrame += 1;
        }
    }



    IEnumerator TimeTrigger()
    {
        if (currentFrame == frameData.Count && state == TimeState.Forward)
            OnTrigger();

        for (int i = 0; i < TimeManager.instance.framePerAction; i++)
        {
            if (state == TimeState.Forward)
            {
                CheckForward();
            }
            else if (state == TimeState.Backward)
            {
                LoadBackward();
            }
            else if (state == TimeState.Pause)
            {
                // SaveData();
            }
            yield return null;
        }
        OnLast();
    }

    public void DisableTimeReact() => timeReactable = false;
    public void EnableTimeReact() => timeReactable = true;
    // public void SetTimeState(TimeState state) => this.state = state;
    protected virtual void OnTrigger() { }

    protected virtual void OnUpdate() { }

    protected virtual void OnLast() { }
}