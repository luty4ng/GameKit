using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player_View : MonoBehaviour
{
    public Animator playerAnimator;
    private bool isWalk;
    private void Awake()
    {
        playerAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        isWalk = false;
    }
    
    private void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }
        playerAnimator.SetBool("isWalk", isWalk);
    }

}
