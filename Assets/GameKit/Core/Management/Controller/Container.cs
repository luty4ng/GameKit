using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameKit
{
    public interface IContainer
    {

    }
    public class Container<T> : MonoBehaviour, IContainer where T : Object
    {
        private List<T> listContainer;
        public virtual T Get(string name)
        {
            return default(T);
        }
        public virtual T GetAt()
        {
            return default(T);
        }
        public virtual void Remove()
        {

        }
        public virtual void RemoveAt()
        {

        }
    }
}
