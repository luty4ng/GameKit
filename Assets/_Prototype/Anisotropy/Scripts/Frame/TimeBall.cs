using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBall : MonoBehaviour
{
    public int timeOffset = 3;
    public Vector3 dir;
    public float speed = 10;
    private void Start()
    {
        StartCoroutine(DestroyInSec());
    }
    private void OnCollisionEnter(Collision other) {
        Debug.Log(other.gameObject.layer);
        if(other.gameObject.layer == LayerMask.NameToLayer("Block"))
        {
            other.gameObject.GetComponent<IChangeFrame>().ChangeFrameBy(timeOffset);
            Destroy(this.gameObject);
        }
    }
    private void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
    IEnumerator DestroyInSec()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
