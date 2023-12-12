using MinoAssistant.Game.Motion.Rotation;
using System;
using System.Linq;

namespace MinoAssistant.Game.Block;

public readonly record struct Mino
{
    public int Size => RelativePositionsDefinition.Length;
    public required Position[] RelativePositionsDefinition { get; init; }
    public required MinoColor MinoColor { get; init; }
    public required MinoColor GhostMinoColor { get; init; }

    public Position[] GetRelativePositions(RotationAmount rotationState) => 
        rotationState.Value switch
        {
            0 => RelativePositionsDefinition,
            90 => RelativePositionsDefinition.Select(p => new Position(p.Y, -p.X)).ToArray(),
            180 => RelativePositionsDefinition.Select(p => new Position(-p.X, -p.Y)).ToArray(),
            270 => RelativePositionsDefinition.Select(p => new Position(-p.Y, p.X)).ToArray(),
            _ => throw new NotImplementedException(),
        };

    public Position[] GetAbsolutePositions(RotationAmount rotationState, Position centerPosition) => GetRelativePositions(rotationState).Select(p => p + centerPosition).ToArray();
}
