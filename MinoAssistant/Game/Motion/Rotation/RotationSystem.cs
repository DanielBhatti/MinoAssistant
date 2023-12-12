namespace MinoAssistant.Game.Motion.Rotation;

public interface RotationSystem
{
    MinoContext? Rotate(MinoField field, RotationAmount rotationAmount);
}
