using MinoAssistant.Board.Block;
using System.Linq;

namespace MinoAssistant.Board.Motion.Rotation
{
    public class ClassicRotationSystem : IRotationSystem
    {
        public MotionResult Rotate(Field field, Mino mino, RotationDirection rotationDirection, Position centerPosition)
        {
            Position[] newPositions = mino.GetRotationPositions(rotationDirection).Select(p => p + centerPosition).ToArray();
            foreach(Position position in newPositions) if (field.IsWithinBounds(position)) if (field[position.X, position.Y].IsFilled) return new MotionResult(mino, MotionType.Fail, centerPosition, centerPosition);
            return new MotionResult(mino, MotionType.Spin, centerPosition, centerPosition);
        }
    }
}
