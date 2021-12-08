using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class BlockMap : AbstractMap
{
    public BlockMap(Vector3Int size, Module borderModule) : base(size, borderModule)
    {

    }

    public Slot GetHighestEntropySlot()
    {
        float entropy = -1;
        Slot slot = null;
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].Collapsed)
                continue;
            if (slotList[i].Entropy > entropy)
            {
                slot = slotList[i];
                entropy = slotList[i].Entropy;
            }
        }
        return slot;
    }

    public Slot GetLowestEntropySlot()
    {
        float entropy = 9999;
        Slot slot = null;
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].Collapsed)
                continue;
            if (slotList[i].Entropy < entropy)
            {
                slot = slotList[i];
                entropy = slotList[i].Entropy;
            }
        }
        return slot;
    }

    public Slot GetSlotRandomSlot()
    {
        List<Slot> unCollapsedSlots = new List<Slot>();
        for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].Collapsed)
                continue;
            unCollapsedSlots.Add(slotList[i]);
        }
        int index = (int)Mathf.Ceil(Random.Range(0f, 1f) * (unCollapsedSlots.Count - 1));
        return unCollapsedSlots[index];
    }
}