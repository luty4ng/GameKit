using UnityEngine;
using System.Collections.Generic;

public class AdjustDisplayLayer : MonoBehaviour
{
    public List<SpriteRenderer> spriteRenderers;
    private int[] initSortingOrder;
    private void Awake()
    {
        spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>(true));
        initSortingOrder = new int[spriteRenderers.Count];
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            initSortingOrder[i] = spriteRenderers[i].sortingOrder;
            spriteRenderers[i].sortingLayerName = "AutoAdjust";
        }
    }

    private void Update()
    {
        for (int i = 0; i < spriteRenderers.Count; i++)
        {
            spriteRenderers[i].sortingOrder = initSortingOrder[i] - (int)(transform.position.y * 30);
        }
    }
}