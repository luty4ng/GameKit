using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GameKit
{
    public sealed class CachedLinkedList<T> : ICollection, ICollection<T>, IEnumerable, IEnumerable<T>
    {
        private readonly LinkedList<T> linkedList;
        private readonly Queue<LinkedListNode<T>> cachedNodes;

        public CachedLinkedList()
        {
            linkedList = new LinkedList<T>();
            cachedNodes = new Queue<LinkedListNode<T>>();
        }

        public int Count
        {
            get
            {
                return linkedList.Count;
            }
        }

        public LinkedListNode<T> First
        {
            get
            {
                return linkedList.First;
            }
        }

        public LinkedListNode<T> Last
        {
            get
            {
                return linkedList.Last;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return (linkedList as ICollection).IsSynchronized;
            }
        }

        public object SyncRoot
        {
            get
            {
                return (linkedList as ICollection).SyncRoot;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (linkedList as ICollection<T>).IsReadOnly;
            }
        }

        void ICollection<T>.Add(T value)
        {
            AddLast(value);
        }

        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            linkedList.AddAfter(node, newNode);
            return newNode;
        }

        public void AddAfter(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            linkedList.AddAfter(node, newNode);
        }

        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            LinkedListNode<T> newNode = AcquireNode(value);
            linkedList.AddBefore(node, newNode);
            return newNode;
        }

        public void AddBefore(LinkedListNode<T> node, LinkedListNode<T> newNode)
        {
            linkedList.AddBefore(node, newNode);
        }

        public LinkedListNode<T> AddFirst(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            linkedList.AddFirst(node);
            return node;
        }

        public void AddFirst(LinkedListNode<T> node)
        {
            linkedList.AddFirst(node);
        }

        public LinkedListNode<T> AddLast(T value)
        {
            LinkedListNode<T> node = AcquireNode(value);
            linkedList.AddLast(node);
            return node;
        }

        public void AddLast(LinkedListNode<T> node)
        {
            linkedList.AddLast(node);
        }

        public void Clear()
        {
            LinkedListNode<T> current = linkedList.First;
            while (current != null)
            {
                ReleaseNode(current);
                current = current.Next;
            }            
            linkedList.Clear();
        }

        public void ClearCachedNodes()
        {
            cachedNodes.Clear();
        }

        private void ReleaseNode(LinkedListNode<T> node)
        {
            node.Value = default(T);
            cachedNodes.Enqueue(node);
        }

        private LinkedListNode<T> AcquireNode(T value)
        {
            LinkedListNode<T> node = null;
            if (cachedNodes.Count > 0)
            {
                node = cachedNodes.Dequeue();
                node.Value = value;
            }
            else
            {
                node = new LinkedListNode<T>(value);
            }
            return node;
        }     

        public LinkedListNode<T> Find(T value)
        {
            return linkedList.Find(value);
        }   

        public LinkedListNode<T> FindLast(T value)
        {
            return linkedList.FindLast(value);
        }

        public bool Contains(T value)
        {
            return linkedList.Contains(value);
        }
        void ICollection<T>.CopyTo(T[] array, int index)
        {
            linkedList.CopyTo(array, index);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            (linkedList as ICollection).CopyTo(array, index);
        }

        public bool Remove(T value)
        {
            LinkedListNode<T> node = linkedList.Find(value);
            if (node != null)
            {
                linkedList.Remove(node);
                ReleaseNode(node);
                return true;
            }
            return false;
        }

        public void Remove(LinkedListNode<T> node)
        {
            linkedList.Remove(node);
            ReleaseNode(node);
        }

        public void RemoveFirst()
        {
            LinkedListNode<T> first = linkedList.First;
            if (first == null)
            {
                throw new Exception("First is invalid.");
            }

            linkedList.RemoveFirst();
            ReleaseNode(first);
        }

        public void RemoveLast()
        {
            LinkedListNode<T> last = linkedList.Last;
            if (last == null)
            {
                throw new Exception("Last is invalid.");
            }

            linkedList.RemoveLast();
            ReleaseNode(last);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(linkedList);
        }

        [StructLayout(LayoutKind.Auto)]
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private LinkedList<T>.Enumerator enumerator;

            internal Enumerator(LinkedList<T> linkedList)
            {
                if (linkedList == null)
                {
                    throw new Exception("Linked list is invalid.");
                }
                enumerator = linkedList.GetEnumerator();
            }

            public T Current
            {
                get
                {
                    return enumerator.Current;
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return enumerator.Current;
                }
            }

            public void Dispose()
            {
                enumerator.Dispose();
            }

            public bool MoveNext()
            {
                return enumerator.MoveNext();
            }

            void IEnumerator.Reset()
            {
                ((IEnumerator<T>)enumerator).Reset();
            }
        }

    }
}
