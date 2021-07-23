using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TopDown : MonoBehaviour
{
    public float speed;
    Vector3 movement;
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        movement = new Vector3(Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed, Input.GetAxisRaw("Vertical") * Time.deltaTime * speed, 0);
        transform.Translate(movement);
        
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
