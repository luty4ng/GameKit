using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TabGroup : MonoBehaviour
{
    public List<TabButton> tabButtons;
    public List<TabPanel> tabPanels;
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabSelected;
    private TabButton current;
    private void Start()
    {
        tabButtons = new List<TabButton>(GetComponentsInChildren<TabButton>(true));
        tabPanels = new List<TabPanel>(GetComponentsInChildren<TabPanel>(true));
        ResetPanel();
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
        ResetPanel();
        current.buttonImage.sprite = tabSelected;
        int index = button.transform.GetSiblingIndex();
        if (index < tabPanels.Count)
            tabPanels[index].gameObject.SetActive(true);
        else
            Debug.LogWarning($"There are tab button still have not been assigned in Object [{this.gameObject.name}].");
    }

    private void ResetToIdle()
    {
        for (int i = 0; i < tabButtons.Count; i++)
        {
            if (tabButtons[i] != current)
                tabButtons[i].buttonImage.sprite = tabIdle;
        }
    }

    private void ResetPanel()
    {
        for (int i = 0; i < tabPanels.Count; i++)
        {
            tabPanels[i].gameObject.SetActive(false);
        }
    }
}