using MinoAssistant.Game.Block;
using MinoAssistant.Game.Motion;
using System.Diagnostics.CodeAnalysis;

namespace MinoAssistant.Game.History;

public readonly record struct MinoInfo
{
    public required int MoveNumber { get; init; }
    public required Mino Mino { get; init; }
    public required MinoContext MotionContext { get; init; }

    [SetsRequiredMembers]
    public MinoInfo(int moveNumber, Mino mino, MinoContext motionContext)
    {
        MoveNumber = moveNumber;
        Mino = mino;
        MotionContext = motionContext;
    }
}
