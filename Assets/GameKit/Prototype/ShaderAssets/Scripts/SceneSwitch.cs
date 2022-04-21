using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SceneSwitch : MonoBehaviour
{
    public Material material;
    public float speed = 10;
    private bool animating = false;
    public float rising = 0;
    private void Start() {
        material.SetFloat("animRate", 0);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animating = true;
        }
        if(animating)
        {
            rising += Time.deltaTime * speed;
            material.SetFloat("animRate", rising);
        }
        
    }
}
