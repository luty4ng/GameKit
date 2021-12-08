using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEditor;
[System.Serializable]
public class FaceAdjacent
{
    public List<string> adjacent = new List<string>();
}

[System.Serializable]
public class Module
{
    public string name;
    public Prototype prototype;
    public GameObject prefab;
    public List<FaceAdjacent> possibleNeighbors;
    public int rotation;
    public int index;
    public Module(GameObject prefab, int rotation, int index)
    {
        this.rotation = rotation;
        this.index = index;
        this.prefab = prefab;
        this.prototype = this.prefab.GetComponent<Prototype>();
        this.name = this.prototype.name + " R" + rotation;
        possibleNeighbors = new List<FaceAdjacent>();
        for (int i = 0; i < 6; i++)
        {
            possibleNeighbors.Add(new FaceAdjacent());
        }
    }


    public bool Fits(int direction, Module module)
    {
        int opositeDir = (direction + 3) % 6;
        if (Orientations.IsHorizontal(direction))
        {
            var f1 = this.prototype.Faces[Orientations.Rotate(direction, this.rotation)] as HorizontalFaceDetails;
            var f2 = module.prototype.Faces[Orientations.Rotate(opositeDir, module.rotation)] as HorizontalFaceDetails;
            return f1.Socket == f2.Socket && (f1.Symmetric || f1.Flipped != f2.Flipped);
        }
        else
        {
            var f1 = this.prototype.Faces[direction] as VerticalFaceDetails;
            var f2 = module.prototype.Faces[opositeDir] as VerticalFaceDetails;
            return f1.Socket == f2.Socket && (f1.Invariant || (f1.Rotation + this.rotation) % 4 == (f2.Rotation + module.rotation) % 4);
        }
    }

    // public FaceDetails GetFace(int direction)
    // {
    //     return this.prototype.Faces[Orientations.Rotate(direction, this.rotation)];
    // }

    public override string ToString()
    {
        return this.name;
    }

}