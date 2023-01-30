using MinoAssistant.Board.Block;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion;
using MinoAssistant.Board.Motion.Rotation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board;

public class GameController
{
    private GameSettings GameSettings { get; }
    private IMinoGenerator MinoGenerator { get; }
    private IRotationSystem RotationSystem { get; }

    public Field Field { get; }
    public Position Origin { get; }

    public Mino CurrentMino { get; private set; }
    public MotionContext CurrentMotionContext { get; private set; }
    public IEnumerable<Position> CurrentMinoAbsolutePositions { get => CurrentMino.GetAbsolutePositions(CurrentMotionContext.RotationState, CurrentMotionContext.CenterPosition); }

    public Mino? HeldMino { get; private set; }
    public bool CanHold { get; private set; } = true;
    public GameState GameState { get; private set; }

    public GameController(GameSettings gameSettings, IMinoGenerator generator, IRotationSystem rotationSystem)
    {
        GameSettings = gameSettings;
        Field = new Field(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue);
        MinoGenerator = generator;
        RotationSystem = rotationSystem;
        Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);
        GameState = GameState.Playing;

        CanHold = true;
        CurrentMino = MinoGenerator.Pop();

        Position[] positions = CurrentMino.GetAbsolutePositions(RotationState.R0, Origin);
        CurrentMotionContext = new MotionContext(positions, MotionType.None, RotationState.R0, Origin);
    }

    public bool SoftDrop()
    {
        Position lowestCenter = GetLowestCenterPosition(Field, CurrentMino.GetRelativePositions(CurrentMotionContext.RotationState), CurrentMotionContext.CenterPosition);
        if (CurrentMotionContext.CenterPosition == lowestCenter)
        {
            ToNextPosition(new MotionContext(CurrentMino.GetAbsolutePositions(CurrentMotionContext.RotationState, lowestCenter), MotionType.SoftDrop, CurrentMotionContext.RotationState, lowestCenter));
            ToNextPiece();
            return true;
        }
        else return false;
    }

    public void HardDrop()
    {
        Position lowestCenter = GetLowestCenterPosition(Field, CurrentMino.GetRelativePositions(CurrentMotionContext.RotationState), CurrentMotionContext.CenterPosition);
        ToNextPosition(new MotionContext(CurrentMino.GetAbsolutePositions(CurrentMotionContext.RotationState, lowestCenter), MotionType.HardDrop, CurrentMotionContext.RotationState, lowestCenter));
        ToNextPiece();
    }

    public void Hold()
    {
        if (CanHold)
        {
            Field.Set(CurrentMinoAbsolutePositions, Field.UnfilledCellValue);
            if (HeldMino == null)
            {
                HeldMino = CurrentMino;
                ToNextPiece(false);
                return;
            }
            else
            {
                (CurrentMino, HeldMino) = (HeldMino.Value, CurrentMino);
                CanHold = false;
                ToNextPosition(new MotionContext(CurrentMinoAbsolutePositions, MotionType.Appear, RotationState.R0, Origin));
            }
        }
    }

    public void MoveMino(MoveDirection moveDirection)
    {
        Position displacement = (0, 0);
        switch (moveDirection)
        {
            case MoveDirection.Up:
                if (GameSettings.IsEditMode)
                {
                    displacement = (1, 0);
                }
                else return;
                break;
            case MoveDirection.Down:
                displacement = (-1, 0);
                break;
            case MoveDirection.Left:
                displacement = (0, -1);
                break;
            case MoveDirection.Right:
                displacement = (0, 1);
                break;
            default: throw new NotImplementedException();
        }
        ToNextPosition(new MotionContext(CurrentMinoAbsolutePositions.Select(p => p + displacement), MotionType.Translation, CurrentMotionContext.RotationState, CurrentMotionContext.CenterPosition + displacement));
    }

    public void Pause() => GameState = GameState.Paused;

    public void Unpause() => GameState = GameState.Playing;

    private bool IsLineFilled(int rowIndex)
    {
        for (int columnIndex = 0; columnIndex < Field.Width; columnIndex++) if (Field.IsFilled((columnIndex, rowIndex))) return false;
        return true;
    }

    private bool ClearLine(int rowIndex)
    {
        if (!IsLineFilled(rowIndex)) return false;
        for (int columnIndex = 0; columnIndex < Field.Width; columnIndex++) Field.Set((columnIndex, rowIndex), GameSettings.UnfilledCellValue);
        for (int newRowIndex = rowIndex + 1; newRowIndex < Field.Height; newRowIndex++)
        {
            for (int columnIndex = 0; columnIndex < Field.Width; columnIndex++)
            {
                Position currentPosition = (columnIndex, rowIndex);
                Position belowPosition = (columnIndex, rowIndex - 1);
                if (Field.IsFilled(currentPosition))
                {
                    ReadOnlyCell cell = Field[currentPosition.X, currentPosition.Y];
                    MinoColor value = cell.Value;
                    Field.Set(currentPosition, Field.UnfilledCellValue);
                    Field.Set(belowPosition, value);
                }
            }
        }
        return true;
    }

    private void ToNextPiece(bool canHold = true)
    {
        CanHold = canHold;
        CurrentMino = MinoGenerator.Pop();
        ToNextPosition(new MotionContext(CurrentMino.GetAbsolutePositions(RotationState.R0, Origin), MotionType.Appear, RotationState.R0, Origin));

        if (!Field.IsFilled(CurrentMinoAbsolutePositions))
        {
            GameState = GameState.GameOver;
            return;
        }
    }

    public void Rotate(RotationDirection rotationDirection) => ToNextPosition(RotationSystem.Rotate(Field, new RotationContext(CurrentMinoAbsolutePositions, CurrentMotionContext.RotationState, rotationDirection, CurrentMotionContext.CenterPosition)));

    private void ToNextPosition(MotionContext nextContext)
    {
        // do it in this order in case the ghost piece intersects with the mino
        if (GameSettings.IsShowingGhostPiece) Field.Set(GetLowestCenterPosition(Field, CurrentMotionContext.AbsolutePositions, CurrentMotionContext.CenterPosition), Field.UnfilledCellValue);
        Field.Set(CurrentMotionContext.AbsolutePositions, Field.UnfilledCellValue);
        Field.Set(nextContext.AbsolutePositions, CurrentMino.MinoColor);
        if (GameSettings.IsShowingGhostPiece) Field.Set(GetLowestCenterPosition(Field, nextContext.AbsolutePositions, nextContext.CenterPosition), CurrentMino.GhostMinoColor);
        CurrentMotionContext = nextContext;
    }

    private static Position GetLowestCenterPosition(Field field, IEnumerable<Position> relativePositions, Position centerPosition)
    {
        while (!relativePositions.Select(p => p + centerPosition + (0, -1)).Any(field.IsFilled)) centerPosition += (0, -1);
        return centerPosition;
    }
}
