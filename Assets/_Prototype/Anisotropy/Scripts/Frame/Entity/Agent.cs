using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using GameKit;

public class Agent : GameEntity
{
    private float horizontal;
    private float vertical;
    private Vector3 targetPos;
    public bool isMoving = false;
    public float stepLen = 1;
    public float interval = 0.2f;
    public Vector2 faceDir = Vector2.down;
    // [Range(0f, 1f)] public float smoothDistance = 0.3f;
    public CommandSet currentCommandSet = CommandSet.emptyCommandSet;

    protected override void OnStart()
    {
        targetPos = this.transform.position;
        isControlAgent = true;
    }

    private void FixedUpdate()
    {
        if (TimeManager.instance.isTimeMoving)
            return;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResourceManager.instance.LoadAsync<GameObject>("Prefab/TimeBall/TimeBall", (GameObject obj) =>
            {
                Debug.Log("TimeBall");
                obj.transform.position = this.transform.position;
                obj.GetComponent<TimeBall>().dir = new Vector3(faceDir.x, 0, faceDir.y);
            });
        }
        float X = 0, Y = 0;
        if (InputManager.instance.GetKeyDown(KeyCode.LeftArrow) || InputManager.instance.GetKeyDown(KeyCode.A))
            X = -1;
        else if (InputManager.instance.GetKeyDown(KeyCode.RightArrow) || InputManager.instance.GetKeyDown(KeyCode.D))
            X = 1;
        else if (InputManager.instance.GetKeyDown(KeyCode.UpArrow) || InputManager.instance.GetKeyDown(KeyCode.W))
            Y = 1;
        else if (InputManager.instance.GetKeyDown(KeyCode.DownArrow) || InputManager.instance.GetKeyDown(KeyCode.S))
            Y = -1;
        if (X == 0 && Y == 0)
            return;



        Vector3 tempPos = targetPos;
        if (tempPos != transform.position)
            transform.position = tempPos;
        bool canMove = MovementManager.instance.CheckAccessibility(tempPos + new Vector3(X * stepLen, 0, Y * stepLen));
        if (!canMove)
            return;
        targetPos = tempPos + new Vector3(X * stepLen, 0, Y * stepLen);
        StartCoroutine(SetTimeMoving());
        faceDir = new Vector2(X, Y);

        this.transform.DOMove(targetPos, interval).OnComplete(() =>
        {
            TimeManager.instance.isTimeMoving = false;
            TimeManager.instance.currentTime += 1;
        });

        TimeManager.instance.TimeTrigger();
    }

    // 在该帧末尾开始时间变动
    IEnumerator SetTimeMoving()
    {
        yield return new WaitForEndOfFrame();
        TimeManager.instance.isTimeMoving = true;
    }

}

