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
        Field = new Field(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue, generator.Minos.Select(m => m.GhostMinoColor));
        MinoGenerator = generator;
        RotationSystem = rotationSystem;
        Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);
        GameState = GameState.Playing;

        CanHold = true;
        CurrentMino = MinoGenerator.Pop();

        Position[] positions = CurrentMino.GetAbsolutePositions(RotationState.R0, Origin);
        CurrentMotionContext = new MotionContext(positions, MotionType.None, RotationState.R0, Origin);

        Field.Set(CurrentMotionContext.AbsolutePositions, CurrentMino.MinoColor);
        //if (GameSettings.IsShowingGhostPiece) AddGhostPiece();
        CurrentMotionContext = CurrentMotionContext;
    }

    public bool SoftDrop()
    {
        Position lowestCenter = GetLowestCenterPosition(CurrentMino.GetRelativePositions(CurrentMotionContext.RotationState), CurrentMotionContext.CenterPosition);
        if (CurrentMotionContext.CenterPosition == lowestCenter)
        {
            ToNextMotionContext(new MotionContext(CurrentMino.GetAbsolutePositions(CurrentMotionContext.RotationState, lowestCenter), MotionType.SoftDrop, CurrentMotionContext.RotationState, lowestCenter));
            ToNextPiece();
            return true;
        }
        else return false;
    }

    public void HardDrop()
    {
        Position lowestCenter = GetLowestCenterPosition(CurrentMino.GetRelativePositions(CurrentMotionContext.RotationState), CurrentMotionContext.CenterPosition);
        ToNextMotionContext(new MotionContext(CurrentMino.GetAbsolutePositions(CurrentMotionContext.RotationState, lowestCenter), MotionType.HardDrop, CurrentMotionContext.RotationState, lowestCenter));
        ToNextPiece();
    }

    public void Hold()
    {
        if (CanHold)
        {
            Field.Set(CurrentMinoAbsolutePositions, Field.DefaultUnfilledCellValue);
            if (HeldMino == null)
            {
                HeldMino = CurrentMino;
                ToNextPiece();
                CanHold = false;
                return;
            }
            else
            {
                (CurrentMino, HeldMino) = (HeldMino.Value, CurrentMino);
                CanHold = false;
                ToNextMotionContext(new MotionContext(CurrentMinoAbsolutePositions, MotionType.Appear, RotationState.R0, Origin));
            }
        }
    }

    public void MoveMino(MoveDirection moveDirection)
    {
        Position displacement = (0, 0);
        switch (moveDirection)
        {
            case MoveDirection.Up:
                if (GameSettings.IsEditMode) displacement = (0, 1);
                else return;
                break;
            case MoveDirection.Down:
                displacement = (0, -1);
                break;
            case MoveDirection.Left:
                displacement = (-1, 0);
                break;
            case MoveDirection.Right:
                displacement = (1, 0);
                break;
            case MoveDirection.None:
                return;
            default: throw new NotImplementedException();
        }
        ToNextMotionContext(new MotionContext(CurrentMinoAbsolutePositions.Select(p => p + displacement), MotionType.Translation, CurrentMotionContext.RotationState, CurrentMotionContext.CenterPosition + displacement));
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
                    Cell cell = Field[currentPosition.X, currentPosition.Y];
                    MinoColor value = cell.Value;
                    Field.Set(currentPosition, Field.DefaultUnfilledCellValue);
                    Field.Set(belowPosition, value);
                }
            }
        }
        return true;
    }

    private void ToNextPiece()
    {
        CurrentMino = MinoGenerator.Pop();

        //if (GameSettings.IsShowingGhostPiece) RemoveGhostPiece();
        CurrentMotionContext = new MotionContext(CurrentMino.GetAbsolutePositions(RotationState.R0, Origin), MotionType.Appear, RotationState.R0, Origin);

        if (Field.IsWithinBounds(CurrentMinoAbsolutePositions) && Field.IsFilled(CurrentMinoAbsolutePositions))
        {
            GameState = GameState.GameOver;
            return;
        }
        else
        {
            Field.Set(CurrentMotionContext.AbsolutePositions, CurrentMino.MinoColor);
            //if (GameSettings.IsShowingGhostPiece) AddGhostPiece();
        }

        for (int rowIndex = 0; rowIndex < Field.Height; rowIndex++) if (IsLineFilled(rowIndex)) ClearLine(rowIndex);
    }

    public void Rotate(RotationDirection rotationDirection) => ToNextMotionContext(RotationSystem.Rotate(Field, new RotationContext(CurrentMinoAbsolutePositions, CurrentMotionContext.RotationState, rotationDirection, CurrentMotionContext.CenterPosition)));

    private void ToNextMotionContext(MotionContext nextContext)
    {
        // do it in this order in case the ghost piece intersects with the mino
        //if (GameSettings.IsShowingGhostPiece) RemoveGhostPiece();
        bool isSuccess = MovePositions(CurrentMotionContext.AbsolutePositions, nextContext.AbsolutePositions, CurrentMino.MinoColor);
        //if (GameSettings.IsShowingGhostPiece) AddGhostPiece();
        if (isSuccess) CurrentMotionContext = nextContext;
    }

    private void RemoveGhostPiece()
    {
        Position lowestCenter = GetLowestCenterPosition(CurrentMotionContext.AbsolutePositions, CurrentMotionContext.CenterPosition);
        foreach (Position position in CurrentMotionContext.RelativePositions.Select(p => p + lowestCenter)) if (!Field.IsWithinBounds(position) && !Field.IsFilled(position)) Field.Set(position, Field.DefaultUnfilledCellValue);
    }

    private void AddGhostPiece()
    {
        Position lowestCenter = GetLowestCenterPosition(CurrentMotionContext.AbsolutePositions, CurrentMotionContext.CenterPosition);
        foreach (Position position in CurrentMotionContext.RelativePositions.Select(p => p + lowestCenter)) if (Field.IsWithinBounds(position) && !Field.IsFilled(position)) Field.Set(position, CurrentMino.GhostMinoColor);
    }

    public Position GetLowestCenterPosition(IEnumerable<Position> relativePositions, Position centerPosition)
    {
        Position lowestCenterPosition = (centerPosition.X, 0);
        while (lowestCenterPosition.Y < GameSettings.OriginY && relativePositions.Select(p => p + lowestCenterPosition).Any(p => !Field.IsWithinBounds(p) || Field.IsFilled(p))) lowestCenterPosition += (0, 1);
        return lowestCenterPosition;
    }

    public bool MovePositions(IEnumerable<Position> initialPositions, IEnumerable<Position> newPositions, MinoColor minoColor)
    {
        if (initialPositions.Count() != newPositions.Count()) throw new ArgumentException($"{nameof(initialPositions)} ({initialPositions}) and {nameof(newPositions)} ({newPositions}) do not have matching sizes.");
        foreach (Position p in newPositions.Except(initialPositions)) if (!Field.IsWithinBounds(p) || Field.IsFilled(p)) return false;
        Field.Set(initialPositions, Field.DefaultUnfilledCellValue);
        Field.Set(newPositions, minoColor);
        return true;
    }
}
