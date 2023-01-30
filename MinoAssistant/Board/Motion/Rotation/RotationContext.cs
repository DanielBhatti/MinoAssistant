using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MinoAssistant.Board.Motion.Rotation;

public record struct RotationContext
{
    public required IEnumerable<Position> AbsolutePositions { get; init; }
    public required RotationState RotationState { get; init; }
    public required RotationDirection RotationDirection { get; init; }
    public required Position CenterPosition { get; init; }

    [SetsRequiredMembers]
    public RotationContext(IEnumerable<Position> absolutePositions, RotationState rotationState, RotationDirection rotationDirection, Position centerPosition)
    {
        AbsolutePositions = absolutePositions;
        RotationState = rotationState;
        RotationDirection = rotationDirection;
        CenterPosition = centerPosition;
    }
}
