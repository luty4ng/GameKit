using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameKit;

public class TriggerEntity : GameEntity
{
    public List<AutoEntity> triggerTarget;
    public List<GameEntity> triggerBy;
    public bool isTriggered = false;

    protected override void OnLast()
    {
        
        for (int i = 0; i < triggerBy.Count; i++)
        {
            if (Mathf.Abs(this.transform.position.x - triggerBy[i].transform.position.x) < 0.02f && Mathf.Abs(this.transform.position.z - triggerBy[i].transform.position.z) < 0.02f)
            {
                for (int j = 0; j < triggerTarget.Count; j++)
                {
                    Debug.Log("Trigger Enter.");
                    isTriggered = true;
                    triggerTarget[j].isActive = isTriggered;
                    TimeManager.instance.AddCommand<CustomCommand<AutoEntity>>(new CustomCommand<AutoEntity>(triggerTarget[i], (AutoEntity entity) =>
                    {
                        entity.isActive = !entity.isActive;
                    }, (AutoEntity entity) =>
                    {
                        entity.isActive = !entity.isActive;
                    }));

                    TimeManager.instance.AddCommand<CustomCommand<TriggerEntity>>(new CustomCommand<TriggerEntity>(this, (TriggerEntity entity) =>
                    {
                        entity.isTriggered = !entity.isTriggered;
                    }, (TriggerEntity entity) =>
                    {
                        entity.isTriggered = !entity.isTriggered;
                    }));
                }
            }
        }
    }

    private void Update()
    {

    }

}