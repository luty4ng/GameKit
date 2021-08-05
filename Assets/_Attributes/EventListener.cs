using UnityEngine;

public class EventListener : MonoBehaviour
{
    private void Start()
    {
        EventCenter.instance.AddEventListener("EventA",()=>{});
        EventCenter.instance.AddEventListener("EventB",()=>{});
        EventCenter.instance.AddEventListener("EventC",()=>{});
    }
}