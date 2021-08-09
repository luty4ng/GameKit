using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<TabPanel> tabPanels;
    [PreviewField] public Sprite tabIdle;
    [PreviewField] public Sprite tabHover;
    [PreviewField] public Sprite tabSelected;
    private TabButton current;
    public void Register(TabButton tabButton)
    {
        if (tabButtons == null)
            tabButtons = new List<TabButton>();
        tabButtons.Add(tabButton);
    }
    private void Start()
    {
        ResetToIdle();
    }

    public void OnTabEnter(TabButton button)
    {
        ResetToIdle();
        if (button != current || current == null)
        {
            button.buttonImage.sprite = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetToIdle();
    }

    public void OnTabSelected(TabButton button)
    {
        current = button;
        ResetToIdle();
        current.buttonImage.sprite = tabSelected;
    }

    private void ResetToIdle()
    {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (tabButtons[i] != current)
                tabButtons[i].buttonImage.sprite = tabIdle;
        }
    }
}