namespace MinoAssistant.Board
{
    public class PlacementState
    {
        public int MoveNumber { get; }
        public MinoType MinoType { get; }
        public Position PlacementCenterPosition { get; }

        public override string ToString() => $"{MinoType}{PlacementCenterPosition}";
    }
}
