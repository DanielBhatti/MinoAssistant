using MinoAssistant.Game.Block;
using MinoAssistant.Game.Motion.Rotation;
using System.Collections.Generic;

namespace MinoAssistant.Game.Motion;

public readonly record struct MinoContext
{
    public MinoContext() { }

    public required Mino Mino { get; init; }
    public required Position CenterPosition { get; init; }
    public RotationAmount RotationAmount { get; init; } = RotationAmount.R0;

    public IEnumerable<Position> AbsolutePositions => Mino.GetAbsolutePositions(RotationAmount, CenterPosition);
    public IEnumerable<Position> RelativePositions => Mino.GetRelativePositions(RotationAmount);
}
