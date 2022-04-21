using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public interface IHeap<T> : IComparable<T>
{
    int index { get; set; }
}

public class Heap<T> where T : IHeap<T>
{
    private T[] items;
    private int currentCount;
    public int Count
    {
        get
        {
            return currentCount;
        }
    }

    public Heap(int maxSize)
    {
        items = new T[maxSize];
    }

    public bool Contains(T item)
    {
        return Equals(items[item.index], item);
    }
    public void Add(T item)
    {
        item.index = currentCount;
        items[currentCount] = item;
        SortUp(item);
        currentCount++;
    }

    public T RemoveFirst()
    {
        T firstItem = items[0];
        currentCount--;
        items[0] = items[currentCount];
        items[0].index = 0;
        SortDown(items[0]);
        return firstItem;
    }

    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    private void SortUp(T item)
    {
        int parentIndex = (item.index - 1) / 2;
        while (true)
        {
            T parentItem = items[parentIndex];
            if (item.CompareTo(parentItem) > 0)
                Swap(item, parentItem);
            else
                break;
            parentIndex = (item.index - 1) / 2;
        }
    }

    private void SortDown(T item)
    {
        while (true)
        {
            int childLeftIndex = item.index * 2 + 1;
            int childRightIndex = item.index * 2 + 2;
            int swapIndex = 0;
            if (childLeftIndex < currentCount)
            {
                swapIndex = childLeftIndex;

                if (childRightIndex < currentCount && items[childLeftIndex].CompareTo(items[childRightIndex]) < 0)
                    swapIndex = childRightIndex;

                if (item.CompareTo(items[swapIndex]) < 0)
                    Swap(item, items[swapIndex]);
                else
                    return;
            }
            else
                return;
        }
    }

    private void Swap(T itemA, T itemB)
    {
        items[itemA.index] = itemB;
        items[itemB.index] = itemA;
        int tmpIndex = itemA.index;
        itemA.index = itemB.index;
        itemB.index = tmpIndex;
    }
}