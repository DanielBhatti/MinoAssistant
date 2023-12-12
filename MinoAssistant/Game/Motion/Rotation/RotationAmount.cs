using System;

namespace MinoAssistant.Game.Motion.Rotation;

public record struct RotationAmount
{
    public static RotationAmount R0 = new(0);
    public static RotationAmount R90 = new(90);
    public static RotationAmount R180 = new(180);
    public static RotationAmount R270 = new(270);

    public int Value { get; }

    private RotationAmount(int value) => Value = value % 360;

    public static RotationAmount operator +(RotationAmount r1, RotationAmount r2)
    {
        var result = ((int)r1.Value + (int)r2.Value) % 360;
        while (result < 0) result += 360;
        return result switch
        {
            0 => RotationAmount.R0,
            90 => RotationAmount.R90,
            180 => RotationAmount.R180,
            270 => RotationAmount.R270,
            _ => throw new NotImplementedException(),
        };
    }

    public static RotationAmount operator *(int multiplier, RotationAmount r) => new(multiplier * (int)r.Value);

    public static RotationAmount operator *(RotationAmount r, int multiplier) => multiplier * r;

    public static RotationAmount operator -(RotationAmount r1, RotationAmount r2) => r1 + -1 * r2;
}
