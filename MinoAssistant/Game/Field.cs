using Microsoft.CodeAnalysis;
using MinoAssistant.Game.Block;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MinoAssistant.Game;

public abstract class Field
{
    public int Width => Cells.GetLength(0);
    public int Height => Cells.GetLength(1);
    protected Cell[,] Cells { get; }
    public MinoColor EmptyCellColor { get; }

    public Cell this[Position p] => Cells[p.X, p.Y];
    public Cell this[int columnIndex, int rowIndex] => Cells[columnIndex, rowIndex];

    public Field(int width, int height, MinoColor emptyCellColor)
    {
        Cells = new Cell[width, height];
        EmptyCellColor = emptyCellColor;
        for(var i = 0; i < Cells.GetLength(0); i++) for(var j = 0; j < Cells.GetLength(1); j++) Cells[i, j] = new Cell(EmptyCellColor);
    }

    public virtual bool IsWithinBounds(Position position) => 0 <= position.X && 0 <= position.Y && position.X < Width && position.Y < Height;
    public bool IsAllWithinBounds(IEnumerable<Position> positions) => positions.All(IsWithinBounds);

    public virtual void Set(Position position, MinoColor minoColor)
    {
        if (IsWithinBounds(position)) Cells[position.X, position.Y].Set(minoColor);
    }
    public void Set(IEnumerable<Position> positions, MinoColor minoColor)
    {
        foreach(var p in positions) Set(p, minoColor);
    }

    public virtual bool IsFilled(Position position) => IsWithinBounds(position) && this[position].Value != EmptyCellColor;
    public bool IsAnyFilled(IEnumerable<Position> positions) => positions.Any(IsFilled);
    public bool IsAllFilled(IEnumerable<Position> positions) => positions.All(IsFilled);
    public bool IsRowFilled(int rowIndex) => IsAllFilled(Enumerable.Range(0, Width).Select(columnIndex => new Position() { X = columnIndex, Y = rowIndex }));

    public virtual void Reset(Position p) => Set(p, EmptyCellColor);
    public void Reset(IEnumerable<Position> positions)
    {
        foreach(var p in positions) Reset(p);
    }
    public virtual void ResetField()
    {
        for(var i = 0; i < Width; i++) for(var j = 0; j < Height; j++) Reset((i, j));
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for(var i = 0; i < Width; i++) _ = sb.AppendLine(string.Join("\t", Enumerable.Range(0, Height).Select(j => this[i, j].Value)));
        return sb.ToString();
    }
}
