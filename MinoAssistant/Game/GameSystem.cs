using MinoAssistant.Game.Motion.Rotation;
using MinoAssistant.Game.Motion;
using System;
using MinoAssistant.Game.Block;
using MinoAssistant.Game.Generator;

namespace MinoAssistant.Game;

public class GameSystem
{
    private GameSettings GameSettings { get; }
    private RotationSystem RotationSystem { get; }
    private MinoGenerator Generator { get; }
    private MinoField Field { get; }

    public Mino CurrentMino { get; private set; }
    public Mino HeldMino { get; private set; }
    public Position Origin => GameSettings.Origin;

    public GameState GameState { get; private set; } = GameState.Playing;
    public Cell this[int columnIndex, int rowIndex] => Field[columnIndex, rowIndex];
    public int Width => Field.Width;
    public int Height => Field.Height;

    public GameSystem(GameSettings gameSettings, MinoGenerator generator, RotationSystem rotationSystem)
    {
        GameSettings = gameSettings;
        RotationSystem = rotationSystem;
        Generator = generator;
        Field = new MinoField(gameSettings.FieldWidth, gameSettings.FieldHeight, MinoColor.None);

        CurrentMino = Generator.Pop();
        _ = Field.AddMino(new MinoContext() { Mino = CurrentMino, CenterPosition = Origin });
    }

    public MotionType Move(MoveDirection moveDirection)
    {
        if(!Field.MinoContext.HasValue) return MotionType.None;
        var result = Field.MoveMino(moveDirection.ToPosition());
        if(result) return MotionType.Translation;
        else return MotionType.Fail;
    }

    public MotionType Rotate(RotationAmount rotationAmount)
    {
        var newContext = RotationSystem.Rotate(Field, rotationAmount);
        return MotionType.None;
    }

    public MotionType Hold()
    {
        throw new NotImplementedException();
    }
}
