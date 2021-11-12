using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Status
{
    Normal,
    Forward,
    Pause,
    Inversion
}
public class TimeControl : MonoBehaviour
{
    public Dictionary<string, TimeControlObj> frameData;
    public Status status;
    private FrameData tempdata;
    public GameObject timeCover;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (status != Status.Pause)
            {
                status = Status.Pause;
                timeCover.SetActive(true);
            }
            else
            {
                status = Status.Normal;
                timeCover.SetActive(false);
            }
        }
        
    }
}


