using System.Collections.Generic;

namespace MinoAssistant.Board.History
{
    public class PlacementStateNode
    {
        public PlacementStateNode? Parent { get; private set; }
        public PlacementState PlacementState { get; }

        private HashSet<PlacementStateNode> _children = new HashSet<PlacementStateNode>();
        public IReadOnlySet<PlacementStateNode> Children { get => _children; }

        public PlacementStateNode(PlacementState placementState) => PlacementState = placementState;

        public void SetParent(PlacementStateNode? placementStateNode) => Parent = placementStateNode;

        public bool AddChild(PlacementStateNode placementStateNode)
        {
            if (_children.Add(placementStateNode))
            {
                placementStateNode.SetParent(this);
                return true;
            }
            else return false;
        }

        public bool RemoveChild(PlacementStateNode placementStateNode)
        {
            if (_children.Remove(placementStateNode))
            {
                placementStateNode.SetParent(null);
                return true;
            }
            else return false;
        }
    }
}
