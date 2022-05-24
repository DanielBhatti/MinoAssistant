namespace MinoAssistant.Board
{
    public readonly record struct Position(int X, int Y)
    {
        public Position Add(int x, int y) => this + new Position(x, y);

        public static Position operator +(Position left, Position right) => new Position(left.X + right.X, left.Y + right.Y);

        public static Position operator -(Position left, Position right) => new Position(left.X - right.X, left.Y - right.Y);

        public override string ToString() => $"({X}, {Y})";
    }
}
