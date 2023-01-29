using System;

namespace MinoAssistant.Board.Motion
{
    [Flags]
    public enum MotionType
    {
        None = 0,
        Fail = 1,
        Translation = 2,
        Rotation = 4,
        Spin = 8,
        Kick = 16,
        Hold = 32,
        SoftDrop = 64,
        HardDrop = 128,
        Appear = 256
    }
}
