using MinoAssistant.Board.Block;

namespace MinoAssistant.Board.Motion.Rotation;

public interface IRotationSystem
{
    MotionContext Rotate(Field field, RotationContext rotationContext);
}
