using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board
{
    public class Field
    {
        public int Width { get => Cells.GetLength(0); }
        public int Height { get => Cells.GetLength(1); }
        public object UnfilledCellValue { get; }
        private Cell[,] Cells { get; }

        // note that for index access the second index is reversed (e.g. 0th component gives the last component)
        // this is so the field goes left to right, bottom to top
        public Cell this[int x, int y] { get => Cells[x, Height - y - 1]; }

        public Field(int width, int height, object unfilledCellValue)
        {
            Cells = new Cell[width, height];
            UnfilledCellValue = unfilledCellValue;
            for (int i = 0; i < Width; i++) for (int j = 0; j < Height; j++) Cells[i, j] = new Cell(UnfilledCellValue, false);
        }

        public bool CanFillCell(Position position) => CanFillCells(new List<Position> { position });
        public bool CanFillCells(ICollection<Position> positions)
        {
            foreach(Position p in positions) if (!IsWithinBounds(p) || Cells[p.X, p.Y].IsFilled) return false;
            return true;
        }

        public bool FillCell(Position position, object value) => FillCells(new List<Position> { position }, value);
        public bool FillCells(ICollection<Position> positions, object value)
        {
            if (!CanFillCells(positions)) return false;

            for (int i = 0; i < positions.Count; i++)
            {
                Position p = positions.ElementAt(i);
                Cells[p.X, p.Y].Fill(value);
            }
            return true;
        }

        public bool CanUnfillCell(Position position) => CanUnfillCells(new List<Position> { position });
        public bool CanUnfillCells(ICollection<Position> positions)
        {
            foreach (Position p in positions) if (!IsWithinBounds(p) || Cells[p.X, p.Y] == null || !Cells[p.X, p.Y].IsFilled) return false;
            return true;
        }

        public bool UnfillCell(Position position) => UnfillCells(new List<Position> { position });
        public bool UnfillCells(ICollection<Position> positions)
        {
            if (!CanUnfillCells(positions)) return false;
            foreach (Position p in positions) Cells[p.X, p.Y].Unfill(UnfilledCellValue);
            return true;
        }

        public bool IsWithinBounds(Position position) => (position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height);
        public bool IsWithinBounds(ICollection<Position> positions) =>
            positions
            .Where(p => !IsWithinBounds(p))
            .Count() == 0;
    }
}
