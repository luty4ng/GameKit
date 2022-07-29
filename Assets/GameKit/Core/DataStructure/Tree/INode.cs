using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.DataStructure
{
    public interface INode
    {
        string Id { get; set; }
        INode Next { get; }
        List<INode> Siblings { get; set; }
        List<INode> Sons { get; set; }
        ITree Tree { get; set; }
        bool IsBranch { get; }
        bool IsLeaf { get; }
        bool IsRoot { get; }
    }
}
