using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum GameBuidinType
{
    Agent,
    Item,
    Effect
}
public class InteractiveObj : MonoBehaviour
{
    public GameBuidinType type = GameBuidinType.Item;
    public Rigidbody rb;
    public Collider coll;
    public Transform followTarget;
    public float moveSpeed = 10;
    public float distanceOffset = 0.2f;
    public bool isPicked = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        coll = this.GetComponent<Collider>();
    }
    public void BePicked(Transform target)
    {
        Debug.Log("BePicked");
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // rb.inertiaTensor = Vector3.one;
        

        isPicked = true;
        followTarget = target;
    }

    public void BeDropped()
    {
        Debug.Log("BeDropped");
        isPicked = false;

        followTarget = null;
    }

    public void BeThrown(Vector3 force)
    {
        Debug.Log("BeThrown");

        isPicked = false;
        StartCoroutine(ThrowNextFrame(force));
    }

    private void Update()
    {
        if (isPicked)
        {
            // rb.isKinematic = true;
            rb.useGravity = false;
            // coll.enabled = false;
            if (followTarget == null)
                return;
            Vector3 dir = followTarget.position - this.transform.position;
            if (dir.magnitude > distanceOffset)
            {
                Debug.Log("MoveBall");
                // rb.MovePosition(transform.position);
                this.transform.Translate(dir.normalized * dir.magnitude * moveSpeed * Time.deltaTime, Space.World);
            }

            if (rb.angularVelocity != Vector3.zero)
                rb.angularVelocity = Vector3.zero;
        }
        else
        {
            // rb.isKinematic = false;
            rb.useGravity = true;
            // coll.enabled = true;
        }
    }
    
    IEnumerator ThrowNextFrame(Vector3 force)
    {
        yield return null;
        // Debug.Log(force * Time.deltaTime);
        rb.AddForce(force * Time.deltaTime);
    }
}