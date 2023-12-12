using System.Collections.Generic;

namespace MinoAssistant.Game.History;

public class Node
{
    public Node? Parent { get; private set; }
    public MinoInfo MinoInfo { get; }

    private readonly List<Node> _children = new();
    public IReadOnlyCollection<Node> Children => _children.AsReadOnly();

    public Node(MinoInfo minoInfo) => MinoInfo = minoInfo;

    public void SetParent(Node? node) => Parent = node;

    public void AddChild(Node node)
    {
        _children.Add(node);
        node.SetParent(this);
    }

    public void RemoveChild(Node node)
    {
        _ = _children.Remove(node);
        node.SetParent(null);
    }
}
