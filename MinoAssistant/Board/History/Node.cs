using System.Collections.Generic;

namespace MinoAssistant.Board.History
{
    public class Node
    {
        public Node? Parent { get; private set; }
        public MinoInfo MinoInfo { get; }

        private List<Node> _children = new List<Node>();
        public IReadOnlyCollection<Node> Children { get => _children.AsReadOnly(); }

        public Node(MinoInfo minoInfo) => MinoInfo = minoInfo;

        public void SetParent(Node? node) => Parent = node;

        public void AddChild(Node node)
        {
            _children.Add(node);
            node.SetParent(this);
        }

        public void RemoveChild(Node node)
        {
            _children.Remove(node);
            node.SetParent(null);
        }
    }
}
