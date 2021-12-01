using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameKit;
public enum AutoMoveType
{
    Unidirection,
    RoundTrip,
    Loop
}
public class AutomaticBlock : AutoEntity
{
    public Vector3 direction;
    public AutoMoveType moveType = AutoMoveType.Unidirection;
    public float stepLen = 1;
    public float interval = 0.2f;
    public int loopInterval = 3;
    public int loopCounter = 0;
    // [Range(0f, 1f)] public float smoothDistance = 0f;
    private Vector3 targetPos;

    protected override void OnStart()
    {
        targetPos = this.transform.position;
    }
    protected override void OnTrigger()
    {
        if(!isActive)
            return;

        Vector3 tempPos = targetPos;
        if (tempPos != transform.position)
            transform.position = tempPos;

        if (moveType == AutoMoveType.Loop)
        {
            if (loopInterval <= 0)
                return;

            if (loopCounter == loopInterval)
            {
                loopCounter = 0;
                direction = -direction;
            }
            loopCounter += 1;
        }


        bool canMove = MovementManager.instance.CheckAccessibility(tempPos + direction.normalized * stepLen);
        if (!canMove)
        {
            if (moveType == AutoMoveType.RoundTrip)
                direction = -direction;
            else
                return;
        }


        targetPos = tempPos + direction.normalized * stepLen;
        this.transform.DOMove(targetPos, interval);
    }

    protected override void OnUpdate()
    {

    }
}