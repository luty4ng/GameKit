using UnityEngine;
using GameKit;
public class EventListener : MonoBehaviour
{
    private void Start()
    {
        EventManager.instance.AddEventListener("EventA",()=>{});
        EventManager.instance.AddEventListener("EventB",()=>{});
        EventManager.instance.AddEventListener("EventC",()=>{});
    }
}