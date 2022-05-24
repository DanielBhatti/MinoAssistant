namespace MinoAssistant.Board
{
    public class ClassicRotationSystem : IRotationSystem
    {
        public MotionResult Rotate(Field field, Mino mino, RotationDirection rotationDirection, Position centerPosition)
        {
            Position[] newPositions = mino.GetRotationPositions(rotationDirection);
            foreach(Position position in newPositions)
            {
                if (field[position.X, position.Y].IsFilled) return new MotionResult(mino, MotionType.Fail, centerPosition, centerPosition);
            }
            return new MotionResult(mino, MotionType.Spin, centerPosition, centerPosition);
        }
    }
}
