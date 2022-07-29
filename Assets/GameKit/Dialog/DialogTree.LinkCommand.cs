using GameKit;
using GameKit.DataStructure;
using DialogNode = GameKit.DataStructure.Node<Dialog>;

public partial class DialogTree
{
    public delegate void PosteriorLink<T>(Node<T> nodeA, string nodeB) where T : NodeType;
    public sealed class LinkCommand<T> : CommandBase where T : NodeType
    {
        public Node<T> nodeA;
        public string targetNode;
        public PosteriorLink<T> command;

        public LinkCommand(Node<T> nodeA, string targetNode, PosteriorLink<T> command)
        {
            this.command = command;
            this.nodeA = nodeA;
            this.targetNode = targetNode;
        }

        public override void Excute()
        {
            command.Invoke(nodeA, targetNode);
        }

        public override string ToString()
        {
            return "链接命令: " + nodeA.ToString() + " to " + targetNode;
        }
    }
}

