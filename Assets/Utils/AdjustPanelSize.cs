using UnityEngine;
using UnityEngine.UI;
public class AdjustPanelSize : MonoBehaviour
{
    public GridLayoutGroup gridLayout;
    public RectTransform myRectTrans;
    private float perModSize;

    private void Start()
    {
        myRectTrans = GetComponent<RectTransform>();
        gridLayout = GetComponentInChildren<GridLayoutGroup>();
        perModSize = gridLayout.cellSize.x + gridLayout.spacing.x;
    }

    private void Update()
    {
        if (20 + gridLayout.transform.childCount * perModSize != myRectTrans.rect.width)
        {
            myRectTrans.sizeDelta = new Vector2(30 + gridLayout.transform.childCount * perModSize, myRectTrans.rect.height);
        }
    }

}