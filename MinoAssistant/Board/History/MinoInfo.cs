using MinoAssistant.Board.Block;
using MinoAssistant.Board.Motion;
using System.Diagnostics.CodeAnalysis;

namespace MinoAssistant.Board.History
{
    public record struct MinoInfo
    {
        public required int MoveNumber { get; init; }
        public required Mino Mino { get; init; }
        public required MotionContext MotionContext { get; init; }

        [SetsRequiredMembers]
        public MinoInfo(int moveNumber, Mino mino, MotionContext motionContext)
        {
            MoveNumber = moveNumber;
            Mino = mino;
            MotionContext = motionContext;
        }
    }
}
