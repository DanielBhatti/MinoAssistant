namespace MinoAssistant.Board
{
    public class Mino
    {
        public int Size { get => RelativePositions.Length; }
        public Position[] OriginalPositions { get; }
        public Position[] RelativePositions { get; private set; }
        public MinoColor MinoColor { get; set; }
        public RotationState RotationState { get; private set; }

        public Mino(Position[] positions, MinoColor minoColor)
        {
            OriginalPositions = positions;
            RelativePositions = positions;
            MinoColor = minoColor;
            RotationState = RotationState.R0;
        }

        public void Rotate(RotationDirection rotationDirection)
        {
            switch (rotationDirection)
            {
                case RotationDirection.None:
                    break;
                case RotationDirection.Clockwise:
                    RotateClockwise();
                    break;
                case RotationDirection.CounterClockwise:
                    RotateCounterClockwise();
                    break;
            }
        }

        public Position[] GetRotationPositions(RotationDirection rotationDirection)
        {
            Position[] rotatedPositions = (Position[])RelativePositions.Clone();

            switch (rotationDirection)
            {
                case RotationDirection.None:
                    break;
                case RotationDirection.Clockwise:
                    for (int i = 0; i < RelativePositions.Length; i++)
                    {
                        int currentX = RelativePositions[i].X;
                        int currentY = RelativePositions[i].Y;
                        rotatedPositions[i] = new Position(currentY, -1 * currentX);
                    }
                    break;
                case RotationDirection.CounterClockwise:
                    for (int i = 0; i < RelativePositions.Length; i++)
                    {
                        int currentX = RelativePositions[i].X;
                        int currentY = RelativePositions[i].Y;
                        rotatedPositions[i] = new Position(-1 * currentY, currentX);
                    }
                    break;
            }
            return rotatedPositions;
        }

        public void RotateClockwise()
        {
            RelativePositions = GetRotationPositions(RotationDirection.Clockwise);
            switch (RotationState)
            {
                case RotationState.R0:
                    RotationState = RotationState.R90;
                    break;
                case RotationState.R90:
                    RotationState = RotationState.R180;
                    break;
                case RotationState.R180:
                    RotationState = RotationState.R270;
                    break;
                case RotationState.R270:
                    RotationState = RotationState.R0;
                    break;
            }
        }

        public void RotateCounterClockwise()
        {
            RelativePositions = GetRotationPositions(RotationDirection.CounterClockwise);
            switch (RotationState)
            {
                case RotationState.R0:
                    RotationState = RotationState.R270;
                    break;
                case RotationState.R90:
                    RotationState = RotationState.R0;
                    break;
                case RotationState.R180:
                    RotationState = RotationState.R90;
                    break;
                case RotationState.R270:
                    RotationState = RotationState.R180;
                    break;
            }
        }
    }
}
