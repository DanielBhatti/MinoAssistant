//using MinoAssistant.Game.Block;
//using MinoAssistant.Game.Generator;
//using MinoAssistant.Game.Motion;
//using MinoAssistant.Game.Motion.Rotation;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace MinoAssistant.Game;

//public class GameController
//{
//    private GameSettings GameSettings { get; }
//    private MinoGenerator MinoGenerator { get; }
//    private RotationSystem RotationSystem { get; }

//    public MinoField Field { get; }
//    public Position Origin { get; }

//    public Mino Mino { get; private set; }
//    public MinoContext? MinoContext => Field.MinoContext;

//    public Mino? HeldMino { get; private set; }
//    public bool CanHold { get; private set; } = true;
//    public GameState GameState { get; private set; }

//    public GameController(GameSettings gameSettings, MinoGenerator generator, RotationSystem rotationSystem)
//    {
//        GameSettings = gameSettings;
//        MinoGenerator = generator;
//        Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);
//        var minoContext = new MinoContext() { Mino = MinoGenerator.Pop(), CenterPosition = Origin };
//        Field = new MinoField(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue, minoContext);
//        RotationSystem = rotationSystem;
//        GameState = GameState.Playing;

//        CanHold = true;
//        Mino = MinoGenerator.Pop();
//    }

//    public bool SoftDrop()
//    {
//        var lowestCenter = GetLowestCenterPosition(Mino.GetRelativePositions(MinoContext.RotationAmount), MinoContext.CenterPosition);
//        if (MinoContext.CenterPosition == lowestCenter)
//        {
//            //ToNextMotionContext(CurrentMinoContext);
//            ToNextPiece();
//            return true;
//        }
//        else return false;
//    }

//    public void HardDrop()
//    {
//        var lowestCenter = GetLowestCenterPosition(Mino.GetRelativePositions(MinoContext.RotationAmount), MinoContext.CenterPosition);
//        //ToNextMotionContext(new MinoContext(CurrentMino.GetAbsolutePositions(CurrentMotionContext.RotationState, lowestCenter), MotionType.HardDrop, CurrentMotionContext.RotationState, lowestCenter));
//        ToNextPiece();
//    }

//    public void Hold()
//    {
//        if (CanHold)
//        {
//            Field.Set(CurrentMinoAbsolutePositions, Field.EmptyCellColor);
//            if (HeldMino == null)
//            {
//                HeldMino = Mino;
//                ToNextPiece();
//                CanHold = false;
//                return;
//            }
//            else
//            {
//                (Mino, HeldMino) = (HeldMino.Value, Mino);
//                CanHold = false;
//                //ToNextMotionContext(new MinoContext(CurrentMinoAbsolutePositions, MotionType.Appear, RotationState.R0, Origin));
//            }
//        }
//    }

//    public void MoveMino(MoveDirection moveDirection)
//    {
//        Position displacement = (0, 0);
//        switch (moveDirection)
//        {
//            case MoveDirection.Up:
//                if (GameSettings.IsEditMode) displacement = (0, 1);
//                else return;
//                break;
//            case MoveDirection.Down:
//                displacement = (0, -1);
//                break;
//            case MoveDirection.Left:
//                displacement = (-1, 0);
//                break;
//            case MoveDirection.Right:
//                displacement = (1, 0);
//                break;
//            case MoveDirection.None:
//                return;
//            default: throw new NotImplementedException();
//        }
//        //ToNextMotionContext(new MinoContext(CurrentMinoAbsolutePositions.Select(p => p + displacement), MotionType.Translation, CurrentMotionContext.RotationState, CurrentMotionContext.CenterPosition + displacement));
//    }

//    public void Pause() => GameState = GameState.Paused;

//    public void Unpause() => GameState = GameState.Playing;

//    private bool IsLineFilled(int rowIndex)
//    {
//        for(var columnIndex = 0; columnIndex < Field.Width; columnIndex++) if(Field.IsFilled((columnIndex, rowIndex))) return false;
//        return true;
//    }
    
//    private bool ClearLine(int rowIndex)
//    {
//        if (!IsLineFilled(rowIndex)) return false;
//        for (var columnIndex = 0; columnIndex < Field.Width; columnIndex++) Field.Set((columnIndex, rowIndex), GameSettings.UnfilledCellValue);
//        for (var newRowIndex = rowIndex + 1; newRowIndex < Field.Height; newRowIndex++)
//        {
//            for(var columnIndex = 0; columnIndex < Field.Width; columnIndex++)
//            {
//                Position currentPosition = (columnIndex, rowIndex);
//                Position belowPosition = (columnIndex, rowIndex - 1);
//                if (Field.IsFilled(currentPosition))
//                {
//                    var cell = Field[currentPosition.X, currentPosition.Y];
//                    var value = cell.Value;
//                    Field.Set(currentPosition, Field.EmptyCellColor);
//                    Field.Set(belowPosition, value);
//                }
//            }
//        }
//        return true;
//    }

//    private void ToNextPiece()
//    {
//        Mino = MinoGenerator.Pop();

//        //if (GameSettings.IsShowingGhostPiece) RemoveGhostPiece();
//        //CurrentMotionContext = new MinoContext(CurrentMino.GetAbsolutePositions(RotationState.R0, Origin), MotionType.Appear, RotationState.R0, Origin);

//        //if (Field.IsWithinBounds(CurrentMinoAbsolutePositions) && Field.IsFilled(CurrentMinoAbsolutePositions))
//        //{
//        //    GameState = GameState.GameOver;
//        //    return;
//        //}
//        //else
//        //{
//        //    _ = Field.Set(CurrentMotionContext.AbsolutePositions, CurrentMino.MinoColor);
//        //    //if (GameSettings.IsShowingGhostPiece) AddGhostPiece();
//        //}

//        for(var rowIndex = 0; rowIndex < Field.Height; rowIndex++) if(IsLineFilled(rowIndex)) _ = ClearLine(rowIndex);
//    }

//    public void Rotate(RotationDirection rotationDirection) => throw new NotImplementedException(); //ToNextMotionContext(RotationSystem.Rotate(Field, new RotationContext(CurrentMinoAbsolutePositions, CurrentMotionContext.RotationAmount, rotationDirection, CurrentMotionContext.CenterPosition)));

//    private void ToNextMotionContext(MinoContext nextContext)
//    {
//        // do it in this order in case the ghost piece intersects with the mino
//        //if (GameSettings.IsShowingGhostPiece) RemoveGhostPiece();
//        var isSuccess = MovePositions(MinoContext.AbsolutePositions, nextContext.AbsolutePositions, Mino.MinoColor);
//        //if (GameSettings.IsShowingGhostPiece) AddGhostPiece();
//        //if (isSuccess) CurrentMotionContext = nextContext;
//    }

//    private void RemoveGhostPiece()
//    {
//        var lowestCenter = GetLowestCenterPosition(MinoContext.AbsolutePositions, MinoContext.CenterPosition);
//        foreach (var position in MinoContext.RelativePositions.Select(p => p + lowestCenter)) if (!Field.IsWithinBounds(position) && !Field.IsFilled(position)) Field.Set(position, Field.EmptyCellColor);
//    }

//    private void AddGhostPiece()
//    {
//        var lowestCenter = GetLowestCenterPosition(MinoContext.AbsolutePositions, MinoContext.CenterPosition);
//        foreach (var position in MinoContext.RelativePositions.Select(p => p + lowestCenter)) if (Field.IsWithinBounds(position) && !Field.IsFilled(position)) Field.Set(position, Mino.GhostMinoColor);
//    }

//    public Position GetLowestCenterPosition(IEnumerable<Position> relativePositions, Position centerPosition)
//    {
//        Position lowestCenterPosition = (centerPosition.X, 0);
//        while (lowestCenterPosition.Y < GameSettings.OriginY && relativePositions.Select(p => p + lowestCenterPosition).Any(p => !Field.IsWithinBounds(p) || Field.IsFilled(p))) lowestCenterPosition += (0, 1);
//        return lowestCenterPosition;
//    }

//    public bool MovePositions(IEnumerable<Position> initialPositions, IEnumerable<Position> newPositions, MinoColor minoColor)
//    {
//        if (initialPositions.Count() != newPositions.Count()) throw new ArgumentException($"{nameof(initialPositions)} ({initialPositions}) and {nameof(newPositions)} ({newPositions}) do not have matching sizes.");
//        foreach (var p in newPositions.Except(initialPositions)) if (!Field.IsWithinBounds(p) || Field.IsFilled(p)) return false;
//        Field.Set(initialPositions, Field.EmptyCellColor);
//        Field.Set(newPositions, minoColor);
//        return true;
//    }
//}
