using UnityEngine;
using GameKit;
using System.Collections;
using System.Collections.Generic;

public class MainTest : MonoBehaviour
{
    private EntityComponent entityComponent;
    private void Start()
    {
        StartCoroutine(ExecuteNextFrame());
    }

    IEnumerator ExecuteNextFrame()
    {
        yield return null;
        entityComponent = GameKitComponentCenter.GetComponent<EntityComponent>();
        entityComponent.ShowEntity(typeof(EntityTest), "Cube", "Geometry", 30, new EntityTestData(2022, 0727));
    }
}