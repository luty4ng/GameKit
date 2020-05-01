using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Add A");
        EventCenter.GetInstance().AddEventListener<EventTrigger>("DoIt", DoA);
        EventCenter.GetInstance().AddEventListener("Win",Win);
    }

    public void Win()
    {

    }
    public void DoA(EventTrigger info)
    {
        Debug.Log("Do A " + info.namea);
    }
}
