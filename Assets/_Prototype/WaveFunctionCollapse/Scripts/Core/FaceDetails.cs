using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public abstract class FaceDetails
{
    public int Socket;
    public virtual void ResetSocket()
    {
        this.Socket = 0;
    }
    public Prototype[] ExcludedNeighbours;
}

[System.Serializable]
public class HorizontalFaceDetails : FaceDetails
{
    public bool Symmetric;
    public bool Flipped;

    public override string ToString()
    {
        return this.Socket.ToString() + (this.Symmetric ? "s" : (this.Flipped ? "F" : ""));
    }

    public override void ResetSocket()
    {
        base.ResetSocket();
        this.Symmetric = false;
        this.Flipped = false;
    }
}

[System.Serializable]
public class VerticalFaceDetails : FaceDetails
{
    public bool Invariant;
    public int Rotation;

    public override string ToString()
    {
        return this.Socket.ToString() + (this.Invariant ? "i" : (this.Rotation != 0 ? "_bcd".ElementAt(this.Rotation).ToString() : ""));
    }

    public override void ResetSocket()
    {
        base.ResetSocket();
        this.Invariant = false;
        this.Rotation = 0;
    }
}