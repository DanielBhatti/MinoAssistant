using MinoAssistant.Board.Motion.Rotation;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MinoAssistant.Board.Block;

public record struct Mino
{
    public int Size { get => RelativePositionsDefinition.Length; }
    public required Position[] RelativePositionsDefinition { get; init; }
    public required MinoColor MinoColor { get; init; }
    public required MinoColor GhostMinoColor { get; init; }

    [SetsRequiredMembers]
    public Mino(IEnumerable<Position> relativePositions, MinoColor minoColor, MinoColor ghostMinoColor)
    {
        RelativePositionsDefinition = relativePositions.ToArray();
        MinoColor = minoColor;
        GhostMinoColor = ghostMinoColor;
    }

    public Position[] GetRelativePositions(RotationState rotationState)
    {
        switch (rotationState.Value)
        {
            case 0: return RelativePositionsDefinition;
            case 90: return RelativePositionsDefinition.Select(p => new Position(p.Y, -p.X)).ToArray();
            case 180: return RelativePositionsDefinition.Select(p => new Position(-p.X, -p.Y)).ToArray();
            case 270: return RelativePositionsDefinition.Select(p => new Position(-p.Y, p.X)).ToArray();
            default: throw new NotImplementedException();
        }
    }

    public Position[] GetAbsolutePositions(RotationState rotationState, Position centerPosition) => GetRelativePositions(rotationState).Select(p => p + centerPosition).ToArray();
}
