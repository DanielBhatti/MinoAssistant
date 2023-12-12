using System;

namespace MinoAssistant.Game.Motion;

public enum MoveDirection
{
    None,
    Up,
    Down,
    Left,
    Right,
}

public static class MoveDirectionExtensions
{
    public static Position ToPosition(this MoveDirection direction) => direction switch
    {
        MoveDirection.None => Position.Zero,
        MoveDirection.Up => (Position)(0, 1),
        MoveDirection.Down => (Position)(0, -1),
        MoveDirection.Left => (Position)(-1, 0),
        MoveDirection.Right => (Position)(1, 0),
        _ => throw new NotImplementedException(),
    };
}