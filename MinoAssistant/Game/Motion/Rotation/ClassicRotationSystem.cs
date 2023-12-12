namespace MinoAssistant.Game.Motion.Rotation;

public class ClassicRotationSystem : RotationSystem
{
    public MinoContext? Rotate(MinoField field, RotationAmount rotationAmount)
    {
        if(!field.MinoContext.HasValue) return field.MinoContext;
        var newContext = new MinoContext() { Mino = field.MinoContext.Value.Mino, CenterPosition = field.MinoContext.Value.CenterPosition, RotationAmount = field.MinoContext.Value.RotationAmount + rotationAmount };
        if(field.IsAnyFilled(newContext.AbsolutePositions)) return field.MinoContext;
        else return newContext;
    }
}
