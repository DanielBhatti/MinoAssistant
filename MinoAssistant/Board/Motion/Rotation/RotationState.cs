using System;

namespace MinoAssistant.Board.Motion.Rotation
{
    public class RotationState
    {
        public static RotationState R0 = new RotationState(0);
        public static RotationState R90 = new RotationState(90);
        public static RotationState R180 = new RotationState(180);
        public static RotationState R270 = new RotationState(270);

        public int Value { get; }

        private RotationState(int value) => Value = value;

        public static RotationState operator +(RotationState r1, RotationState r2)
        {
            int result = ((int)r1.Value + (int)r2.Value) % 360;
            while (result < 0) result += 360;
            switch (result)
            {
                case 0: return RotationState.R0;
                case 90: return RotationState.R90;
                case 180: return RotationState.R180;
                case 270: return RotationState.R270;
                default: throw new NotImplementedException();
            }
        }

        public static RotationState operator *(int multiplier, RotationState r) => new RotationState(multiplier * (int)r.Value);

        public static RotationState operator *(RotationState r, int multiplier) => multiplier * r;

        public static RotationState operator -(RotationState r1, RotationState r2) => r1 + (-1 * r2);
    }
}
