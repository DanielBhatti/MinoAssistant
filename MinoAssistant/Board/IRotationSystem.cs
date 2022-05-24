namespace MinoAssistant.Board
{
    public interface IRotationSystem
    {
        MotionResult Rotate(Field field, Mino mino, RotationDirection rotationDirection, Position centerPosition);
    }
}
