using UnityEngine;
using UnityEngine.UI;
public enum PanelAdjustType
{
    Horizontal,
    Vertical
}

[ExecuteInEditMode]
public class AdaptiveSize_UI : MonoBehaviour
{
    private GridLayoutGroup gridLayout;
    private RectTransform myRectTrans;
    private float perModSize;
    public PanelAdjustType adjustType;
    public int maxWidth = 3;
    public int maxHeight = 3;


    private void Start()
    {
        myRectTrans = GetComponent<RectTransform>();
        gridLayout = GetComponentInChildren<GridLayoutGroup>();
        perModSize = gridLayout.cellSize.x + gridLayout.spacing.x;
    }

    private void Update()
    {
        if (adjustType == PanelAdjustType.Horizontal)
        {
            if (gridLayout.padding.left + gridLayout.padding.right + gridLayout.transform.childCount * perModSize != myRectTrans.rect.width)
            {
                myRectTrans.sizeDelta = new Vector2((gridLayout.padding.left + gridLayout.padding.right + perModSize * Mathf.Clamp(gridLayout.transform.childCount, 1, maxWidth)),
                                                     gridLayout.padding.top + gridLayout.padding.bottom + perModSize * Mathf.CeilToInt((float)gridLayout.transform.childCount / (float)maxHeight));
            }
        }
        else if (adjustType == PanelAdjustType.Vertical)
        {
            if (gridLayout.padding.top + gridLayout.padding.bottom + gridLayout.transform.childCount * perModSize != myRectTrans.rect.height)
            {
                myRectTrans.sizeDelta = new Vector2(gridLayout.padding.left + gridLayout.padding.right + perModSize * Mathf.CeilToInt(gridLayout.transform.childCount / maxWidth),
                                                    gridLayout.padding.top + gridLayout.padding.bottom + perModSize * Mathf.Clamp(gridLayout.transform.childCount, 1, maxHeight));
            }
        }

    }

}