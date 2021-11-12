using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingGround : MonoBehaviour
{
    public float speed;
    public GameObject cubes;
    private float horizontal;
    private float vertical;
    private Vector3 movement;

    public Material material;

    private void Start() {
        // materials = cubes.GetComponentsInChildren<MeshRenderer>();
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        movement = new Vector3(horizontal * Time.deltaTime * speed, 0, vertical * Time.deltaTime * speed);

        transform.Translate(movement);
        if (movement.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        if (movement.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        material.SetVector("_agentPos", transform.position);
    }
}
