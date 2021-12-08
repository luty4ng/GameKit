using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;
public abstract class AbstractMap
{
    public const float BLOCK_SIZE = 1f;
    public const int BUFFER_SIZE = 3000;
    public static System.Random randomGen;
    public List<Slot> slotList;
    public static Slot BorderSlot;
    public AbstractMap()
    {
        randomGen = new System.Random();
    }

    public AbstractMap(Vector3Int size, Module borderModule)
    {
        randomGen = new System.Random();
        slotList = new List<Slot>();
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                for (int z = 0; z < size.z; z++)
                {
                    slotList.Add(new Slot(new Vector3Int(x, y, z), this));
                }
            }
        }
        BorderSlot = new Slot(new Vector3Int(-1, -1, -1), this, borderModule);
    }

    public bool AllCollapse
    {
        get
        {
            if (slotList.Count == 0)
            {
                Debug.LogWarning("Slot Map Count == 0");
                return false;
            }
            for (int i = 0; i < slotList.Count; i++)
            {
                if (!slotList[i].Collapsed)
                    return false;
            }
            return true;
        }
    }

    public Slot GetSlot(int index)
    {
        if (index >= slotList.Count)
        {
            Debug.LogWarning("Invalid Index for SlotIndex");
            return null;
        }
        return slotList[index];
    }

    public Slot GetSlot(Vector3Int pos)
    {
        return slotList.Find(slot => slot.position == pos);
    }
}
