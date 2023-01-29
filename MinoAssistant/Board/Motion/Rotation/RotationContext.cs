using System.Collections.Generic;

namespace MinoAssistant.Board.Motion.Rotation;

public record struct RotationContext
{
    public required IEnumerable<Position> AbsolutePositions { get; init; }
    public required RotationState RotationState { get; init; }
    public required RotationDirection RotationDirection { get; init; }
    public required Position CenterPosition { get; init; }
}
