using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameKit.DataStructure
{
    public class Node<T> : INode where T : NodeType
    {
        private string id;
        private INode parent;
        private List<INode> sons;
        private List<INode> siblings;
        private ITree tree;

        public T nodeEntity;
        public bool IsSBranch = false;
        public bool IsCBranch = false;
        public string Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public bool IsRoot
        {
            get
            {
                if (parent == this)
                    return true;
                return false;
            }
        }

        public bool IsBranch
        {
            get
            {
                if (sons.Count > 1)
                    return true;
                return false;
            }
        }

        public bool IsLeaf
        {
            get
            {
                if (sons.Count == 0)
                    return true;
                return false;
            }
        }

        public INode Next
        {
            get
            {
                if (sons.Count > 0)
                    return sons[0];
                return null;
            }
        }

        public List<INode> Siblings
        {
            get
            {
                return siblings;
            }
            set
            {
                siblings = value;
            }
        }

        public List<INode> Sons
        {
            get
            {
                return sons;
            }
            set
            {
                sons = value;
            }
        }

        public ITree Tree
        {
            get
            {
                return tree;
            }
            set
            {
                tree = value;
            }
        }

        public Node(string id, ITree tree = null, bool isRoot = false)
        {
            this.Id = id;
            this.sons = new List<INode>();
            this.siblings = new List<INode>();
            this.tree = tree;
            parent = this;
        }

        public Node(ITree tree = null, bool isRoot = false)
        {
            this.Id = Utility.Random.GetRandomID();
            this.sons = new List<INode>();
            this.siblings = new List<INode>();
            this.tree = tree;
            parent = this;
        }
        
        public void OnStart()
        {
            nodeEntity.OnEnter();
        }
        public void OnUpdate()
        {
            nodeEntity.OnUpdate();
        }
        public void OnFinish()
        {
            nodeEntity.OnFinish();
        }

        public override string ToString()
        {
            return nodeEntity.ToString();
        }
    }
}
