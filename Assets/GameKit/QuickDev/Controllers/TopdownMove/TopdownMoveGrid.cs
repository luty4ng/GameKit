using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TopdownMoveGrid : MonoBehaviour
{
    public enum Orientation
    {
        XYZ,
        XZY
    }
    private float horizontal;
    private float vertical;
    private Vector3 targetPos;
    public bool isMoving = false;
    public float stepLen = 1;
    public float interval = 0.2f;
    [Range(0f, 1f)] public float smoothDistance = 0.3f;
    public Orientation orientation = Orientation.XYZ;

    private void Start() {
        targetPos = this.transform.position;
    }

    void Update()
    {
        if (isMoving)
        {
            if ((targetPos - transform.position).magnitude < smoothDistance)
                isMoving = false;
            else
                return;
        }
        float X = 0, Y = 0;
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            X = -1;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            X = 1;
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            Y = 1;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            Y = -1;
        if (X == 0 && Y == 0)
            return;
        Vector3 tempPos = targetPos;
        if(tempPos!=transform.position)
            transform.position = tempPos;

        switch (orientation)
        {
            case Orientation.XYZ:
                targetPos = tempPos + new Vector3(X * stepLen, Y * stepLen, 0);
                break;
            case Orientation.XZY:
                targetPos = tempPos + new Vector3(X * stepLen, 0, Y * stepLen);
                break;
            default:
                break;
        }
        this.transform.DOMove(targetPos, interval);
        isMoving = true;
    }
}
