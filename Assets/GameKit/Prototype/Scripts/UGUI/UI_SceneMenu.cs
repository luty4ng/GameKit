using UnityEngine;
using GameKit;

public class UI_SceneMenu : UIGroup
{
    public Animator animator;
    private bool isOpen = false;
    protected override void OnStart()
    {
        animator = GetComponent<Animator>();
    }
    public void SwitchSceneMenu()
    {
        if(isOpen)
        {
            isOpen = false;
            animator.SetTrigger("Close");
        }
        else
        {
            isOpen = true;
            animator.SetTrigger("Open");
        }
        
    }

}