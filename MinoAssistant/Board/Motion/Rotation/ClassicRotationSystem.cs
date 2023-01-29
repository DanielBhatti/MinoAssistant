using MinoAssistant.Board.Block;
using System;
using System.Linq;

namespace MinoAssistant.Board.Motion.Rotation
{
    public class ClassicRotationSystem : IRotationSystem
    {
        public MotionContext Rotate(Field field, RotationContext rotationContext)
        {
            RotationState newRotationState;
            switch (rotationContext.RotationDirection)
            {
                case RotationDirection.None:
                    return new MotionContext(rotationContext.AbsolutePositions, MotionType.None, rotationContext.RotationState, rotationContext.CenterPosition);
                case RotationDirection.Clockwise:
                    newRotationState = rotationContext.RotationState + RotationState.R90;
                    break;
                case RotationDirection.CounterClockwise:
                    newRotationState = rotationContext.RotationState - RotationState.R90;
                    break;
                default: throw new NotImplementedException();
            }

            Position[] newPositions;
            switch (newRotationState.Value)
            {
                case 0:
                    newPositions = rotationContext.AbsolutePositions.Select(p => p + rotationContext.CenterPosition).ToArray();
                    break;
                case 90:
                    newPositions = rotationContext.AbsolutePositions.Select(p => (p.Y, -1 * p.X) + rotationContext.CenterPosition).ToArray();
                    break;
                case 180:
                    newPositions = rotationContext.AbsolutePositions.Select(p => (-1 * p.X, -1 * p.Y) + rotationContext.CenterPosition).ToArray();
                    break;
                case 270:
                    newPositions = rotationContext.AbsolutePositions.Select(p => (-1 * p.Y, p.X) + rotationContext.CenterPosition).ToArray();
                    break;
                default:
                    throw new NotImplementedException();
            }

            foreach (Position p in newPositions) if (field.IsFilled(p)) return new MotionContext(rotationContext.AbsolutePositions, MotionType.Fail, rotationContext.RotationState, rotationContext.CenterPosition);

            return new MotionContext(rotationContext.AbsolutePositions, MotionType.Rotation, newRotationState, rotationContext.CenterPosition);
        }
    }
}
