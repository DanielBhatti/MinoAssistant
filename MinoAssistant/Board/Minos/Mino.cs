namespace MinoAssistant.Board.Minos
{
    public class Mino
    {
        public int Size { get => Positions.Length; }
        public Position[] Positions { get; private set; }

        public Mino(Position[] positions) => Positions = positions;

        public Position[] RotateClockwise()
        {
            for(int i = 0; i < Positions.Length; i++)
            {
                int currentX = Positions[i].X;
                int currentY = Positions[i].Y;
                Positions[i] = new Position(currentY, -1 * currentX);
            }
            return Positions;
        }

        public Position[] RotateCounterClockwise()
        {
            for (int i = 0; i < Positions.Length; i++)
            {
                int currentX = Positions[i].X;
                int currentY = Positions[i].Y;
                Positions[i] = new Position(-1 * currentY, currentX);
            }
            return Positions;
        }
    }
}
