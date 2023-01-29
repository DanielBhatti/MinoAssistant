using MinoAssistant.Board.Block;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion;
using MinoAssistant.Board.Motion.Rotation;
using System.Collections.Generic;
using System.Linq;

namespace MinoAssistant.Board;

public class GameController
{
    private GameSettings GameSettings { get; }
    private IGenerator Generator { get; }
    private IRotationSystem RotationSystem { get; }

    public Field Field { get; }
    public Position Origin { get; }

    public Mino Mino { get; private set; }
    public MotionContext MotionContext { get; private set; }

    public Mino? HeldMino { get; private set; }
    public bool CanHold { get; private set; } = true;
    public GameState GameState { get; private set; }

    public GameController(GameSettings gameSettings, IGenerator generator, IRotationSystem rotationSystem)
    {
        GameSettings = gameSettings;
        Field = new Field(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue);
        Generator = generator;
        RotationSystem = rotationSystem;
        Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);
        GameState = GameState.Playing;

        CanHold = true;
        Mino = Generator.Pop();

        Position[] positions = Mino.GetAbsolutePositions(RotationState.R0, Origin);
        MotionContext = new MotionContext(positions, MotionType.None, RotationState.R0, Origin);
    }

    public void HardDrop()
    {
        Position lowestCenter = GetLowestCenterPosition(Field, Mino.GetRelativePositions(MotionContext.RotationState), MotionContext.CenterPosition);
        PlaceMino(MotionContext, new MotionContext(Mino.GetAbsolutePositions(MotionContext.RotationState, lowestCenter), MotionType.HardDrop, MotionContext.RotationState, lowestCenter));
    }

    //public MotionType Hold()
    //{
    //    Field.SetValues(CurrentMinoAbsolutePositions, GameSettings.UnfilledCellValue);
    //    RemoveGhostPiece();

    //    if (CanHold)
    //    {
    //        if (HeldMino is null)
    //        {
    //            HeldMino = CurrentMino;
    //            ContinueToNextPiece();
    //        }
    //        else
    //        {
    //            (HeldMino, CurrentMino) = (CurrentMino, HeldMino.Value);
    //            CanHold = false;
    //            CurrentMinoCenterPosition = Origin;
    //        }

    //        Field.SetValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
    //        AddGhostPiece();
    //        return MotionType.Hold;

    //    }
    //    Field.SetValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
    //    AddGhostPiece();
    //    return MotionType.Fail;
    //}

    //public MotionContext MoveMino(MoveDirection moveDirection)
    //{
    //    throw new NotImplementedException();
    //    Field.SetValues(CurrentMinoAbsolutePositions, GameSettings.UnfilledCellValue);
    //    RemoveGhostPiece();
    //    MotionContext motionResult;
    //    //switch (moveDirection)
    //    //{
    //    //    case MoveDirection.None:
    //    //        motionResult = new MotionResult(CurrentMino, MotionType.None, CurrentMinoCenterPosition, CurrentMinoCenterPosition);
    //    //        break;
    //    //    case MoveDirection.Left:
    //    //        motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(-1, 0));
    //    //        break;
    //    //    case MoveDirection.Right:
    //    //        motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(1, 0));
    //    //        break;
    //    //    case MoveDirection.Down:
    //    //        motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(0, -1));
    //    //        break;
    //    //    case MoveDirection.ClockwiseRotation:
    //    //        motionResult = RotationSystem.Rotate(Field, CurrentMino, RotationDirection.Clockwise, CurrentMinoCenterPosition);
    //    //        break;
    //    //    case MoveDirection.CounterClockwiseRotation:
    //    //        motionResult = RotationSystem.Rotate(Field, CurrentMino, RotationDirection.CounterClockwise, CurrentMinoCenterPosition);
    //    //        break;
    //    //    default:
    //    //        motionResult = new MotionResult(CurrentMino, MotionType.Fail, CurrentMinoCenterPosition, CurrentMinoCenterPosition);
    //    //        break;
    //    //}

    //    //RotationDirection rotationDirection;
    //    //switch (moveDirection)
    //    //{
    //    //    case MoveDirection.Left:
    //    //    case MoveDirection.Right:
    //    //    case MoveDirection.Down:
    //    //        rotationDirection = RotationDirection.None;
    //    //        break;
    //    //    case MoveDirection.ClockwiseRotation:
    //    //        rotationDirection = RotationDirection.Clockwise;
    //    //        break;
    //    //    case MoveDirection.CounterClockwiseRotation:
    //    //        rotationDirection = RotationDirection.CounterClockwise;
    //    //        break;
    //    //    default:
    //    //        rotationDirection = RotationDirection.None;
    //    //        break;
    //    //}

    //    //if (Field.CanFillCells(GetMinoAbsolutePositions(CurrentMino, motionResult.NewCenterPosition, rotationDirection)))
    //    //{
    //    //    CurrentMinoCenterPosition = motionResult.NewCenterPosition;
    //    //    CurrentMino.Rotate(rotationDirection);
    //    //}

    //    //Field.SetValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
    //    //AddGhostPiece();
    //    //return motionResult;
    //}

    //public void Pause() => GameState = GameState.Paused;

    //public void Unpause() => GameState = GameState.Playing;

    //private bool IsLineFilled(int rowIndex)
    //{
    //    for (int i = 0; i < Field.Width; i++) if (Field.IsFilled((i, rowIndex))) return false;
    //    return true;
    //}

    //private bool ClearLine(int rowIndex)
    //{
    //    if (!IsLineFilled(rowIndex)) return false;
    //    for (int j = 0; j < Field.Width; j++) Field.SetValue((j, rowIndex), GameSettings.UnfilledCellValue);
    //    for (int i = rowIndex + 1; i < Field.Height; i++)
    //    {
    //        for (int j = 0; j < Field.Width; j++)
    //        {
    //            Position currentPosition = (i, j);
    //            Position belowPosition = (i - 1, j);
    //            if (Field.IsFilled(currentPosition))
    //            {
    //                ReadOnlyCell cell = Field[currentPosition.X, currentPosition.Y];
    //                MinoColor value = cell.Value;
    //                Field.SetValue(currentPosition, GameSettings.UnfilledCellValue);
    //                Field.SetValue(belowPosition, value);
    //            }
    //        }
    //    }
    //    return true;
    //}

    //private Position[] GetMinoAbsolutePositions(Mino mino, Position centerPosition, RotationDirection rotationDirection) => mino.GetRotationPositions(rotationDirection).Select(rp => rp + centerPosition).ToArray();

    //private Position GetGhostPieceCenterPosition()
    //{
    //    Position ghostPieceCenterPosition = CurrentMinoCenterPosition;
    //    while (Field.CanFillCells(GetMinoAbsolutePositions(CurrentMino, ghostPieceCenterPosition.Add(0, -1), RotationDirection.None))) ghostPieceCenterPosition = ghostPieceCenterPosition.Add(0, -1);
    //    return ghostPieceCenterPosition;
    //}

    //private void RemoveGhostPiece()
    //{
    //    foreach (Position position in GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None)) Field.SetValue(position, GameSettings.UnfilledCellValue);
    //}

    //private void AddGhostPiece()
    //{
    //    foreach (Position position in GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None)) Field.SetValue(position, GameSettings.GhostPieceValue);
    //}

    private void ToNextPiece()
    {
        CanHold = true;
        Mino = Generator.Pop();

        Position[] positions = Mino.GetAbsolutePositions(MotionContext.RotationState, MotionContext.CenterPosition);
        if (!Field.IsFilled(positions))
        {
            GameState = GameState.GameOver;
            return;
        }
        else PlaceMino(MotionContext, new MotionContext(Mino.GetAbsolutePositions(RotationState.R0, Origin), MotionType.Appear, RotationState.R0, Origin));
    }

    //public void Rotate(RotationDirection rotationDirection)
    //{
    //    switch (rotationDirection)
    //    {
    //        case RotationDirection.None:
    //            break;
    //        case RotationDirection.Clockwise:
    //            RotateClockwise();
    //            break;
    //        case RotationDirection.CounterClockwise:
    //            RotateCounterClockwise();
    //            break;
    //    }
    //}

    private void PlaceMino(MotionContext currentContext, MotionContext nextContext)
    {
        // do it in this order in case the ghost piece intersects with the mino
        if (GameSettings.IsShowingGhostPiece) Field.Set(GetLowestCenterPosition(Field, currentContext.AbsolutePositions, currentContext.CenterPosition), Field.UnfilledCellValue);
        Field.Set(currentContext.AbsolutePositions, Field.UnfilledCellValue);
        Field.Set(nextContext.AbsolutePositions, Mino.MinoColor);
        if (GameSettings.IsShowingGhostPiece) Field.Set(GetLowestCenterPosition(Field, nextContext.AbsolutePositions, nextContext.CenterPosition), Mino.GhostMinoColor);
        MotionContext = nextContext;
        ToNextPiece();
    }

    private static Position GetLowestCenterPosition(Field field, IEnumerable<Position> relativePositions, Position centerPosition)
    {
        while (!relativePositions.Select(p => p + centerPosition + (0, -1)).Any(field.IsFilled)) centerPosition += (0, -1);
        return centerPosition;
    }
}
