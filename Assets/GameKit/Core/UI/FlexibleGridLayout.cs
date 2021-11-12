using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FlexibleGridLayout : LayoutGroup
{
    public enum LayoutType
    {
        Uniform,
        Vertical,
        Horizontal,
        FixedHeight,
        FixedWidth
    }
    public LayoutType layoutType;
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public bool fitX;
    public bool fitY;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        if (layoutType == LayoutType.Horizontal || layoutType == LayoutType.Vertical || layoutType == LayoutType.Uniform)
        {
            fitX = true;
            fitY = true;
            float childSqr = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(childSqr);
            columns = Mathf.CeilToInt(childSqr);
        }


        if (layoutType == LayoutType.Horizontal || layoutType == LayoutType.FixedWidth)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float)columns);
        }
        else if (layoutType == LayoutType.Vertical || layoutType == LayoutType.FixedHeight)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = ((parentWidth - padding.left - padding.right) - (spacing.x * (float)(columns - 1))) / (float)columns;
        float cellHeight = ((parentHeight - padding.top - padding.bottom) - (spacing.y * (float)(rows - 1))) / (float)rows;

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;
            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        // throw new System.NotImplementedException();
    }

    public override void SetLayoutHorizontal()
    {
        // throw new System.NotImplementedException();
    }

    public override void SetLayoutVertical()
    {
        // throw new System.NotImplementedException();
    }
}