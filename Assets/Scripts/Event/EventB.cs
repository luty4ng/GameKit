using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Add B");
        EventCenter.GetInstance().AddEventListener<EventTrigger>("DoIt", DoB);
    }
    public void DoB(EventTrigger info)
    {
        Debug.Log("Do B");
    }
}
