using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopdownMove : MonoBehaviour
{
    public enum Orientation
    {
        XYZ,
        XZY
    }
    private float horizontal;
    private float vertical;
    private Vector3 movement;
    public float speed = 10;
    
    public Orientation orientation = Orientation.XYZ;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        switch (orientation)
        {
            case Orientation.XYZ:
                Movement2D();
                break;
            case Orientation.XZY:
                Movement3D();
                break;
            default:
                break;
        }
    }

    void Movement2D()
    {
        movement = new Vector3(horizontal * Time.deltaTime * speed, vertical * Time.deltaTime * speed, 0);
        transform.Translate(movement);
    }

    void Movement3D()
    {
        movement = new Vector3(horizontal, 0, vertical) * Time.deltaTime * speed;
        transform.Translate(movement);
    }


}
