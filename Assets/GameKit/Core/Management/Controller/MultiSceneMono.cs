using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;

namespace GameKit
{
    public class MultiSceneMono : MonoBehaviour
    {
        public List<SceneCollection> collections;
        public SceneCollection currentLevel;
        public Material material;
        public float speed = 10;
        private bool animating = false;
        public float rising = 0;
        private void Start()
        {
            if (collections.Count > 0)
                currentLevel = collections[0];
            currentLevel.LoadDefaultScene();
            // material.SetFloat("animRate", 0);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentLevel.LoadPreviousScene();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentLevel.LoadNextScene();
            }

            // if(Input.GetKeyDown(KeyCode.Space))
            // {
            //     animating = true;
            // }
            // if(animating)
            // {
            //     rising += Time.deltaTime * speed;
            //     material.SetFloat("animRate", rising);
            // }
        }

    }
}