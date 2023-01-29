using MinoAssistant.Board.Motion.Rotation;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace MinoAssistant.Board.Motion
{
    public record struct MotionContext
    {
        public required IEnumerable<Position> AbsolutePositions { get; init; }
        public required MotionType SourceMotionType { get; init; }
        public required RotationState RotationState { get; init; }
        public required Position CenterPosition { get; init; }
        public IEnumerable<Position> RelativePositions { get; }

        [SetsRequiredMembers]
        public MotionContext(IEnumerable<Position> absolutePositions, MotionType motionType, RotationState rotationState, Position centerPosition)
        {
            AbsolutePositions = absolutePositions;
            SourceMotionType = motionType;
            RotationState = rotationState;
            CenterPosition = centerPosition;
            RelativePositions = AbsolutePositions.Select(p => p - centerPosition).ToArray();
        }
    }
}
