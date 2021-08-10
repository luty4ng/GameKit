using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public TabPanel tabPanel;
    public Image buttonImage;

    private void Awake()
    {
        tabGroup = GetComponentInParent<TabGroup>();
        buttonImage = GetComponent<Image>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
}