using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameKit;
using UnityEngine.UI;

public enum TimeControlMode
{
    Anisotropy,
    ActiveTimeState
}

public class TimeManager : MonoBehaviour
{
    public int currentTime;
    public float interval;
    public bool isTimeMoving = false;
    public Text modeText;
    public int framePerAction;
    public CommandSet tempCommandSet;
    [SerializeField] private List<GameEntity> entities;
    public static TimeManager instance { get; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        Application.targetFrameRate = framePerAction * 60;
        currentTime = 0;
        InputManager.instance.SetActive(true);
    }

    private void Start()
    {

    }

    public void AddEntity(GameEntity entity)
    {
        if (entities == null)
            entities = new List<GameEntity>();
        entities.Add(entity);

    }

    public void Clear()
    {
        entities.Clear();
    }

    public void InsertCommand<T>(int timeSlot, T command) where T : Command
    {
        IEnumerable<GameEntity> target = (from entity in entities where entity == command.GetEntity() select entity);
    }

    public void AddCommand<T>(T command) where T : Command
    {
        if (tempCommandSet == null)
            tempCommandSet = new CommandSet();
        tempCommandSet.AddCommand(command);
        if (command.GetEntity().currentFrame != 0)
            command.GetEntity().frameData[command.GetEntity().currentFrame - 1].commandSet = tempCommandSet;
        else
            command.GetEntity().frameData[command.GetEntity().currentFrame].commandSet = tempCommandSet;
    }

    private void Update()
    {
        if (modeText.text == "模式三：移动时时间不变")
        {
            if (InputManager.instance.GetKey(KeyCode.Q))
            {
                isTimeMoving = true;
            }
            else if (InputManager.instance.GetKey(KeyCode.E))
            {
                isTimeMoving = true;
            }
        }

        if (isTimeMoving)
            return;



        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            modeText.text = "模式一：移动时时间正向";
            ChangeEntityMode(TimeState.Forward);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            modeText.text = "模式二：移动时时间逆向";
            ChangeEntityMode(TimeState.Backward);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            modeText.text = "模式三：移动时时间不变";
            ChangeEntityMode(TimeState.Pause);
        }

    }

    private void ChangeEntityMode(TimeState state)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            entities[i].state = state;
        }
    }

    public void TimeTrigger()
    {
        EventManager.instance.EventTrigger(EventConfig.TimeChange.ToString());
        if (tempCommandSet == null)
            tempCommandSet = new CommandSet();
        tempCommandSet.Clear();
    }

    public void AutoCorrection()
    {

    }
}


