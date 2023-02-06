using Microsoft.CodeAnalysis;
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
        public MinoColor UnfilledCellValue { get; }

        public Cell this[int columnIndex, int rowIndex] { get => Cells[columnIndex, rowIndex]; }

        public Field(int width, int height, MinoColor unfilledCellValue)
        {
            Cells = new Cell[width, height];
            UnfilledCellValue = unfilledCellValue;
            for (int i = 0; i < Width; i++) for (int j = 0; j < Height; j++) Cells[i, j] = new Cell(UnfilledCellValue);
        }

        public bool Set(Position position, MinoColor value) => Set(new Position[] { position }, value);
        public bool Set(IEnumerable<Position> positions, MinoColor value)
        {
            if (!IsWithinBounds(positions)) return false;
            foreach (Position p in positions) Cells[p.X, p.Y].Set(value);
            return true;
        }

        public bool IsFilled(Position position) => UnfilledCellValue != this[position.X, position.Y].Value;
        public bool IsFilled(IEnumerable<Position> positions) => !positions.Any(p => !IsFilled(p));

        public bool IsWithinBounds(Position position) => (0 <= position.X && 0 <= position.Y && position.X < Width && position.Y < Height);
        public bool IsWithinBounds(IEnumerable<Position> positions) => !positions.Any(p => !IsWithinBounds(p));
    }
}
