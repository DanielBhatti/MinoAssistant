using MinoAssistant.Board.Block;

namespace MinoAssistant.Board.Motion.Rotation
{
    public interface IRotationSystem
    {
        MotionResult Rotate(Field field, Mino mino, RotationDirection rotationDirection, Position centerPosition);
    }
}
