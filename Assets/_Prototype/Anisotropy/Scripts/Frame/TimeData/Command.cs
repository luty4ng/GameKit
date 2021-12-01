using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public interface ICommand { }
public abstract class Command : ICommand
{
    protected GameEntity entity;
    public GameEntity GetEntity() => entity;
    public void SetEntity(GameEntity entity) => this.entity = entity;
    public abstract void Do();
    public abstract void DoReverse();
}

public class CreateInstance : Command
{
    Vector3 position;
    Quaternion quaternion;
    Transform parent;
    public CreateInstance(GameEntity entity, Vector3 position, Quaternion quaternion, Transform parent)
    {
        this.entity = entity;
        this.position = position;
        this.quaternion = quaternion;
        this.parent = parent;
    }
    public override void Do()
    {
        GameObject instance = GameObject.Instantiate<GameObject>(entity.gameObject, position, quaternion, parent);
        entity.gameObject.SetActive(false);
        instance.GetComponent<ITimeReactable>().DisableTimeReact();
    }

    public override void DoReverse()
    {
        entity.gameObject.SetActive(false);
        entity.GetComponent<ITimeReactable>().DisableTimeReact();
    }
}


public class EnableTimeReact : Command
{
    System.Action<GameEntity> action;
    public EnableTimeReact(GameEntity entity)
    {
        this.entity = entity;
    }
    public override void Do()
    {
        entity.GetComponent<ITimeReactable>().EnableTimeReact();
    }

    public override void DoReverse()
    {
        entity.GetComponent<ITimeReactable>().DisableTimeReact();
    }
}

public class CustomCommand<T> : Command where T : GameEntity
{
    System.Action<T> action;
    System.Action<T> actionReverse;
    public CustomCommand(T entity, System.Action<T> action, System.Action<T> actionRe)
    {
        this.entity = entity;
        this.action = action;
        this.actionReverse = actionRe;
    }
    public override void Do()
    {
        action?.Invoke(entity as T);
    }

    public override void DoReverse()
    {
        actionReverse?.Invoke(entity as T);
    }
}



