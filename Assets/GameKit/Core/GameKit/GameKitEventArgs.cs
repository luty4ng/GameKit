using UnityEngine;
using System;

namespace GameKit
{
    public abstract class GameKitEventArgs : EventArgs, IReference
    {
        public abstract string Id { get; }
        public abstract void Clear();
    }
}

