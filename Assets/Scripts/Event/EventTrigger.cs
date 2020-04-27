using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public string namea = " good";
    void Start()
    {
        //DoIt();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            DoIt();
        }
    }
    // Update is called once per frame
    void DoIt()
    {
        Debug.Log("Begin Do it");
        EventCenter.GetInstance().EventTrigger("DoIt", this);
    }
}
