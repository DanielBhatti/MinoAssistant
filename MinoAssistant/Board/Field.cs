using Microsoft.CodeAnalysis;
using MinoAssistant.Board.Block;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinoAssistant.Board
{
    public class Field
    {
        public int Width { get => Cells.GetLength(0); }
        public int Height { get => Cells.GetLength(1); }
        private Cell[,] Cells { get; }
        public MinoColor DefaultUnfilledCellValue { get; }
        public HashSet<MinoColor> UnfilledCellValues { get; } = new();

        public Cell this[int columnIndex, int rowIndex] { get => Cells[columnIndex, rowIndex]; }

        public Field(int width, int height, MinoColor unfilledCellValue) : this(width, height, unfilledCellValue, new List<MinoColor>() { unfilledCellValue }) { }
        public Field(int width, int height, MinoColor defaultUnfilledCellValue, IEnumerable<MinoColor> unfilledCellValues)
        {
            Cells = new Cell[width, height];
            DefaultUnfilledCellValue = defaultUnfilledCellValue;
            UnfilledCellValues.Add(defaultUnfilledCellValue);
            foreach (MinoColor minoColor in unfilledCellValues) UnfilledCellValues.Add(minoColor);
            for (int i = 0; i < Width; i++) for (int j = 0; j < Height; j++) Cells[i, j] = new Cell(DefaultUnfilledCellValue);
        }

        public bool Set(Position position, MinoColor value) => Set(new Position[] { position }, value);
        public bool Set(IEnumerable<Position> positions, MinoColor value)
        {
            if (!IsWithinBounds(positions)) return false;
            foreach (Position p in positions) Cells[p.X, p.Y].Set(value);
            return true;
        }

        public bool IsFilled(Position position) => !UnfilledCellValues.Contains(this[position.X, position.Y].Value);
        public bool IsFilled(IEnumerable<Position> positions) => positions.Any(IsFilled);

        public bool IsWithinBounds(Position position) => (0 <= position.X && 0 <= position.Y && position.X < Width && position.Y < Height);
        public bool IsWithinBounds(IEnumerable<Position> positions) => positions.All(IsWithinBounds);

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Width; i++) sb.AppendLine(string.Join("\t", Enumerable.Range(0, Height).Select(j => this[i, j].Value)));
            return sb.ToString();
        }

        public void Reset()
        {
            for (int i = 0; i < Width; i++) for (int j = 0; j < Height; j++) Set((i, j), DefaultUnfilledCellValue);
        }
    }
}
