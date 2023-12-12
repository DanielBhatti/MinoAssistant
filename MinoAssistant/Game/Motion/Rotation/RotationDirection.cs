using System;

namespace MinoAssistant.Game.Motion.Rotation;

public enum RotationDirection
{
    None,
    Clockwise,
    CounterClockwise
}

public static class RotationDirectionExtensions
{
    public static RotationAmount ToRotationAmount(this RotationDirection rotationDirection) =>
        rotationDirection switch
        {
            RotationDirection.None => RotationAmount.R0,
            RotationDirection.Clockwise => RotationAmount.R90,
            RotationDirection.CounterClockwise => RotationAmount.R270,
            _ => throw new NotImplementedException(),
        };
}