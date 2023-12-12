using System.Diagnostics.CodeAnalysis;

namespace MinoAssistant.Game;

public readonly record struct Position
{
    public required int X { get; init; }
    public required int Y { get; init; }

    [SetsRequiredMembers]
    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Position Add(int x, int y) => this + new Position(x, y);

    public static Position operator +(Position left, Position right) => new Position(left.X + right.X, left.Y + right.Y);

    public static Position operator -(Position left, Position right) => new Position(left.X - right.X, left.Y - right.Y);

    public static implicit operator Position((int, int) t) => new Position(t.Item1, t.Item2);

    public static Position Zero = new(0, 0);

    public override string ToString() => $"({X}, {Y})";
}
