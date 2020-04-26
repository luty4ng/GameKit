using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventC : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Add C");
        EventCenter.GetInstance().AddEventListener("DoIt", DoC);
    }
    public void DoC(object info)
    {
        Debug.Log("Do C");
    }
}
