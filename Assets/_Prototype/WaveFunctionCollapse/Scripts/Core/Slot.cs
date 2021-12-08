using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[System.Serializable]
public class Slot
{
    public Vector3Int position;
    public List<Module> modules;
    private AbstractMap map;
    public Module outputModule;
    public GameObject instance;
    public bool Collapsed = false;
    public float Entropy
    {
        get
        {
            return modules.Count;
        }
    }


    public Slot(Vector3Int position, AbstractMap map)
    {
        this.position = position;
        this.map = map;
        this.modules = new List<Module>(WFCGenerator.instance.moduleData.Modules);
    }

    public Slot(Vector3Int position, AbstractMap map, Module borderModule)
    {
        this.position = position;
        this.map = map;
        this.modules = new List<Module>();
        this.modules.Add(borderModule);
        this.outputModule = borderModule;
        this.Collapsed = true;
    }

    public Slot GetNeighbor(int direction)
    {
        Slot neighborSlot = this.map.GetSlot(this.position + Orientations.Direction[direction]);
        if (neighborSlot != null)
            return neighborSlot;
        else
            return AbstractMap.BorderSlot;
    }

    public void Collapse(Module module)
    {
        if (this.Collapsed)
        {
            Debug.LogWarning("Trying to collapse already collapsed slot.");
            return;
        }
        this.outputModule = module;
        this.modules.Clear();
        this.modules.Add(this.outputModule);
        this.Collapsed = true;
        Debug.Log("CollapseAt: " + this.position);
    }

    public void CollapseRandom()
    {
        if (this.Collapsed)
        {
            Debug.LogWarning("Slot is already collapsed.");
            return;
        }


        // int index = (int)Mathf.Ceil(UnityEngine.Random.Range(0f, 1f) * (modules.Count - 1));
        // this.Collapse(modules[index]);
        
        float probabilitySum = this.modules.Select(module => module.prototype.Probability).Sum();
        float roll = (float)(BlockMap.randomGen.NextDouble() * probabilitySum);
        float counter = 0;
        foreach (var module in this.modules)
        {
            counter += module.prototype.Probability;
            if (counter >= roll)
            {
                this.Collapse(module);
                return;
            }
        }
        this.Collapse(this.modules.First());
    }

    public void AddModule(Module module)
    {
        modules.Add(module);
    }

    public void RemoveModule(Module module)
    {
        if (modules.Contains(module))
            modules.Remove(module);
    }

    public List<string> GetPossibleNeighborAt(int direction)
    {
        List<string> allPossibleModule = new List<string>();
        for (int i = 0; i < modules.Count; i++)
        {
            if (modules[i].possibleNeighbors[direction].adjacent == null)
                continue;
            if (modules[i].possibleNeighbors[direction].adjacent.Count == 0)
                continue;
            foreach (var neighbor in modules[i].possibleNeighbors[direction].adjacent)
            {
                if (!allPossibleModule.Contains(neighbor))
                    allPossibleModule.Add(neighbor);
            }
        }
        return allPossibleModule;
    }

    public override int GetHashCode()
    {
        return this.position.GetHashCode();
    }

    public static bool CheckBorderSlot(Slot slot)
    {
        if (slot.position.x == -1 && slot.position.y == -1 && slot.position.z == -1)
        {
            return true;
        }
        return false;
    }
}
