namespace MinoAssistant.Board.Minos
{
    public class Mino
    {
        public int Size { get => RelativePositions.Length; }
        public Position[] RelativePositions { get; private set; }

        public Mino(Position[] positions) => RelativePositions = positions;

        public Position[] RotateClockwise()
        {
            for(int i = 0; i < RelativePositions.Length; i++)
            {
                int currentX = RelativePositions[i].X;
                int currentY = RelativePositions[i].Y;
                RelativePositions[i] = new Position(currentY, -1 * currentX);
            }
            return RelativePositions;
        }

        public Position[] RotateCounterClockwise()
        {
            for (int i = 0; i < RelativePositions.Length; i++)
            {
                int currentX = RelativePositions[i].X;
                int currentY = RelativePositions[i].Y;
                RelativePositions[i] = new Position(-1 * currentY, currentX);
            }
            return RelativePositions;
        }
    }
}
