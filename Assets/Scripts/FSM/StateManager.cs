using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StateManager : BaseManager<StateManager> {
    private List<States> states = new List<States>();
    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private States currentState;
    public States CurrentState { get { return currentState; } }

    public void AddState(States s)
    {
        if(s == null)
            Debug.LogError("FSM ERROR: State不可为空");
        
        if(states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.stateid;
            return;
        }

        foreach(States i in states)
        {
            if(i.stateid == s.stateid)
            {
                Debug.LogError("FSM ERROR: 无法添加状态 " + s.stateid.ToString() + " 因为该状态已存在");
                return;
            }
        }
        states.Add(s);
    }

    public void DeleteState(StateID id)
    {
        if(id == StateID.NullState)
        {
            Debug.LogError("FSM ERROR: 状态ID 不可为空");
            return;
        }

        foreach(States i in states)
        {
            if(i.stateid == id)
            {
                states.Remove(i);
                return;
            }
        }
        Debug.LogError("FSM ERROR: 无法删除状态 " + id.ToString() + ". 状态列表中不存在");
    }

    /// <summary>
    ///  基于Transition约束进行状态转换
    /// </summary>
    /// <param name="target"></param>
    public void PerformTransition(Transition target)
    {
        if(target == Transition.NUllTransition)
        {
            Debug.Log("FSM ERROR: 转换不可为空");
            return;
        }
        StateID targetID = currentState.GetState(target);
        if(targetID == StateID.NullState)
        {
            Debug.LogError("FSM ERROR: 状态 " + currentStateID.ToString() + " 不存在目标状态 " +  
                           " - 转换： " + target.ToString());  
            return; 
        }

        
        foreach(States i in states)
        {
            if(i.stateid == targetID)
            {
                currentState.OnExit();
                currentStateID = targetID;
                currentState = i;
                currentState.OnEnter();
            }
        }
    }

    /// <summary>
    /// 不受约束的状态转换
    /// </summary>
    /// <param name="s"></param>
    public void SetCurrentState(States s)
    {
        currentState.OnExit();
        currentStateID = s.stateid;
        currentState = s;
        currentState.OnEnter();
    }

    public void Excute<T>(T controller)
    {
        currentState.Excute<T>(controller);
    }
}