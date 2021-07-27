using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Player_TopDown : MonoBehaviour
{
    public float speed;
    [ShowInInspector, DisplayAsString] private float horizontal;
    [ShowInInspector, DisplayAsString] private float vertical;
    public Player_View playerView;
    public LayerMask interactLayer;
    private Rigidbody2D playerRigid;
    public CircleCollider2D playerColl;
    public LayerMask collLayer;

    private Vector3 movement;
    private Vector2 holderDir;

    public bool rightColl;
    public bool leftColl;
    public bool upColl;
    public bool downColl;
    
    void Start()
    {

    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontal * Time.deltaTime * speed, vertical * Time.deltaTime * speed, 0);
        rightColl = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(0.25f, -0.25f), new Vector2(0.1f, 0.1f), 0, collLayer);
        leftColl = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(-0.25f, -0.25f), new Vector2(0.1f, 0.1f), 0, collLayer);
        upColl = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(0, 0), new Vector2(0.1f, 0.1f), 0, collLayer);
        downColl = Physics2D.OverlapBox((Vector2)transform.position + new Vector2(0, -0.5f), new Vector2(0.1f, 0.1f), 0, collLayer);

        if((upColl && vertical == 1) || (downColl && vertical == -1))
            movement.y = 0;
        
        if((leftColl && horizontal == -1) || (rightColl && horizontal == 1))
            movement.x = 0;

        transform.Translate(movement);
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(0.25f, -0.25f), new Vector2(0.1f, 0.1f));
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(-0.25f, -0.25f), new Vector2(0.1f, 0.1f));
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(0, 0), new Vector2(0.1f, 0.1f));
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(0, -0.5f), new Vector2(0.1f, 0.1f));
    }
}
