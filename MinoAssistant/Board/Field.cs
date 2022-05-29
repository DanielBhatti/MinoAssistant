using MinoAssistant.Board.Block;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board
{
    public class Field
    {
        public int Width { get => Cells.GetLength(0); }
        public int Height { get => Cells.GetLength(1); }
        private Cell[,] Cells { get; }

        // note that for index access the second index is reversed (e.g. 0th component gives the last component)
        // this is so the field goes left to right, bottom to top
        public ReadOnlyCell this[int x, int y] { get => new ReadOnlyCell(Cells[x, Height - y - 1]); }

        public Field(int width, int height, MinoColor unfilledCellValue)
        {
            Cells = new Cell[width, height];
            for (int i = 0; i < Width; i++) for (int j = 0; j < Height; j++) Cells[i, j] = new Cell(unfilledCellValue, false);
        }

        public bool SetCellValue(Position position, MinoColor value) => SetCellValues(new Position[] { position }, value);
        public bool SetCellValues(ICollection<Position> positions, MinoColor value)
        {
            if (!IsWithinBounds(positions)) return false;
            foreach (Position p in positions) Cells[p.X, p.Y].SetValue(value);
            return true;
        }

        public bool IsFilledCell(Position position) => !CanFillCell(position);
        public bool IsFilledCells(ICollection<Position> positions) => !CanFillCells(positions);

        public bool CanFillCell(Position position) => CanFillCells(new Position[] { position });
        public bool CanFillCells(ICollection<Position> positions)
        {
            foreach(Position p in positions) if (!IsWithinBounds(p) || Cells[p.X, p.Y].IsFilled) return false;
            return true;
        }

        public bool FillCell(Position position, MinoColor value) => FillCells(new Position[] { position }, value);
        public bool FillCells(ICollection<Position> positions, MinoColor value)
        {
            if (!CanFillCells(positions)) return false;

            for (int i = 0; i < positions.Count; i++)
            {
                Position p = positions.ElementAt(i);
                Cells[p.X, p.Y].Fill(value);
            }
            return true;
        }

        public bool CanUnfillCell(Position position) => CanUnfillCells(new Position[] { position });
        public bool CanUnfillCells(ICollection<Position> positions)
        {
            foreach (Position p in positions) if (!IsWithinBounds(p) || Cells[p.X, p.Y] == null || !Cells[p.X, p.Y].IsFilled) return false;
            return true;
        }

        public bool UnfillCell(Position position, MinoColor value) => UnfillCells(new Position[] { position }, value);
        public bool UnfillCells(ICollection<Position> positions, MinoColor value)
        {
            if (!CanUnfillCells(positions)) return false;
            foreach (Position p in positions) Cells[p.X, p.Y].Unfill(value);
            return true;
        }

        public bool IsWithinBounds(Position position) => (position.X >= 0 && position.Y >= 0 && position.X < Width && position.Y < Height);
        public bool IsWithinBounds(ICollection<Position> positions) =>
            positions
            .Where(p => !IsWithinBounds(p))
            .Count() == 0;
    }
}
