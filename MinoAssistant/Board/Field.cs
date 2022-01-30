using MinoAssistant.Board.Minos;

namespace MinoAssistant.Board
{
    public class Field
    {
        public int Width { get => Cells.GetLength(0); }
        public int Height { get => Cells.GetLength(1); }
        public Cell[,] Cells { get; }

        public Field(int width, int height) => Cells = new Cell[width, height];

        public bool CanSetMinoPosition(Mino mino, Position position)
        {
            if (Cells[position.X, position.Y].IsFilled) return false;
            foreach(Position relativePosition in mino.RelativePositions)
            {
                Position p = relativePosition + position;
                if (Cells[p.X, p.Y].IsFilled) return false;
            }
            return true;
        }

        public void SetMinoPosition(Mino mino, Position position)
        {
            if (!CanSetMinoPosition(mino, position)) return;

            foreach(Position relativePosition in mino.RelativePositions)
            {
                Position p = relativePosition + position;
                Cells[p.X, p.Y].Fill(true);
            }
        }
    }
}
