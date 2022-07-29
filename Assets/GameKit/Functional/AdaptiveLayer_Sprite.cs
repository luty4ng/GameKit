using UnityEngine;
using System.Collections.Generic;

public class AdaptiveLayer_Sprite : MonoBehaviour
{
    public string sortingLayer = "AutoAdjust";
    public static int capacityRange = 30;
    private List<SpriteRenderer> spriteRenderers;
    private int[] initSortingOrder;

    private void Awake()
    {
        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>(true));
        initSortingOrder = new int[spriteRenderers.Count];
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            initSortingOrder[i] = spriteRenderers[i].sortingOrder;
            spriteRenderers[i].sortingLayerName = sortingLayer;
        }
    }

    private void Update()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sortingOrder = initSortingOrder[i] - (int)(transform.position.y * capacityRange);
        }
    }
}