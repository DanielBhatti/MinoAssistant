namespace MinoAssistant.Board
{
    public struct Position
    {
        public int X { get; }
        public int Y { get; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position Create(int x, int y) => new Position(x, y);

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Position operator +(Position left, Position right) => new Position(left.X + right.X, left.Y + right.Y);

        public static Position operator -(Position left, Position right) => new Position(left.X - right.X, left.Y - right.Y);
    }
}
