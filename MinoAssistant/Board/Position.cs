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

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public override bool Equals(object obj)
        {
            Position p = (Position)obj;
            if (p.X == X && p.Y == Y) return true;
            else return false;
        }

        public static Position operator +(Position left, Position right) => new Position(left.X + right.X, left.Y + right.Y);

        public static Position operator -(Position left, Position right) => new Position(left.X - right.X, left.Y - right.Y);

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 19;
                hash = hash * 7 + X.GetHashCode();
                hash = hash * 31 + Y.GetHashCode();
                return hash;
            }
        }
    }
}
