using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventA : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Add A");
        EventCenter.GetInstance().AddEventListener("DoIt", DoA);
    }
    public void DoA(object info)
    {
        Debug.Log("Do A" + (info as EventTrigger).namea);
    }
}
