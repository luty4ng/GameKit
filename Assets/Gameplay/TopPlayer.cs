using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopPlayer : MonoBehaviour
{
    public float speed;

    Animator animator;
    Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, 0);

        transform.Translate(movement);//移动
        
        if (movement != Vector3.zero)//动画
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
        
        if (movement.x > 0)//翻脸
            transform.localScale = new Vector3(1, 1, 1);
        if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
