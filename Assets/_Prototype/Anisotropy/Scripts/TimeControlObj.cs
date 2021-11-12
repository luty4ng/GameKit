using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControlObj : MonoBehaviour
{
    public Stack<FrameData> TimeData;
    public Animator anim;
    public Rigidbody rb;
    private FrameData tempFrameData;
    [SerializeField] private bool isInversion;
    [SerializeField] private bool keyUp;
    [SerializeField] private bool keyDown;

    private void Start()
    {
        tempFrameData = new FrameData();
        TimeData = new Stack<FrameData>();
        // anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void SaveData()
    {
        FrameData data = new FrameData();
        data.position = this.transform.position;
        data.rotation = this.transform.rotation.eulerAngles;
        data.scale = this.transform.localScale;
        data.velocity = this.GetComponent<Rigidbody>().velocity;
        TimeData.Push(data);
    }

    private FrameData LoadData()
    {
        if (TimeData.Count > 1)
            return TimeData.Pop();
        else
            return TimeData.Peek();
    }

    private void AssignData(FrameData status)
    {
        rb.isKinematic = true;
        
        this.transform.position = status.position;
        this.transform.localScale = status.scale;
        this.transform.eulerAngles = status.rotation;
        rb.velocity = status.velocity;
    }

    private void FixedUpdate()
    {
        if (keyDown)
        {
            tempFrameData = LoadData();
            if (tempFrameData != null)
                AssignData(tempFrameData);
        }
        else
        {
            SaveData();
        }
    }

    private void Update()
    {
        keyDown = Input.GetKey(KeyCode.LeftShift);
        keyUp = Input.GetKeyUp(KeyCode.LeftShift);

        if(keyUp)
        {
            TimeData.Clear();
            rb.isKinematic = false;
        }
    }
}