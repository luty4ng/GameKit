using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Add B");
        EventCenter.GetInstance().AddEventListener("DoIt", DoB);
    }
    public void DoB(object info)
    {
        Debug.Log("Do B");
    }
}
