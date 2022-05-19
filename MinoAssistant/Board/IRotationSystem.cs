namespace MinoAssistant.Board
{
    public interface IRotationSystem
    {
        Position Rotate(Field field, Mino mino, RotationDirection rotationDirection, Position centerPosition);
    }
}
