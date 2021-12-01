using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class FrameData
{
    public Vector3 position { get; set; }
    public Vector3 scale { get; set; }
    public Vector3 rotation { get; set; }
    public CommandSet commandSet { get; set; }
}