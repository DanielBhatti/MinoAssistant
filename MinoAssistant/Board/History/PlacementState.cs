using MinoAssistant.Board.Block;

namespace MinoAssistant.Board.History
{
    public class PlacementState
    {
        public int MoveNumber { get; }
        public MinoType MinoType { get; }
        public Position PlacementCenterPosition { get; }

        public PlacementState(int moveNumber, MinoType minoType, Position placementCenterPosition)
        {
            MoveNumber = moveNumber;
            MinoType = minoType;
            PlacementCenterPosition = placementCenterPosition;
        }

        public override string ToString() => $"{MinoType}{PlacementCenterPosition}";
    }
}
