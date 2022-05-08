using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board
{
    public class Field
    {
        public int Width { get => Cells.GetLength(0); }
        public int Height { get => Cells.GetLength(1); }
        private Cell[,] Cells { get; }

        public Cell this[int x, int y] { get => Cells[x, y]; }

        public Field(int width, int height) => Cells = new Cell[width, height];

        public bool CanSetPositions(ICollection<Position> positions)
        {
            foreach(Position p in positions) if (!IsWithinBounds(p) || Cells[p.X, p.Y].IsFilled) return false;
            return true;
        }

        public bool SetPositions(ICollection<Position> positions, ICollection<object>? values)
        {
            if (values is null) values = positions.Select(p => (object)true).ToList();
            if (positions.Count != values.Count) return false;
            if (!CanSetPositions(positions)) return false;

            for (int i = 0; i < positions.Count; i++)
            {
                Position p = positions.ElementAt(i);
                Cells[p.X, p.Y].Fill(values.ElementAt(i));
            }
            return true;
        }

        public bool CanRemovePositions(ICollection<Position> positions)
        {
            foreach (Position p in positions) if (!IsWithinBounds(p) || !Cells[p.X, p.Y].IsFilled) return false;
            return true;
        }

        public bool RemovePositions(ICollection<Position> positions)
        {
            if (!CanRemovePositions(positions)) return false;
            foreach (Position p in positions) Cells[p.X, p.Y].Unfill();
            return true;
        }

        public bool IsWithinBounds(ICollection<Position> positions) =>
            positions
            .Where(p => !IsWithinBounds(p))
            .Count() == 0;

        public bool IsWithinBounds(Position position) => (position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height);
    }
}
