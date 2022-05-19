﻿using System.Linq;

namespace MinoAssistant.Board
{
    public class Game
    {
        private GameSettings GameSettings { get; }
        private IGenerator Generator { get; }
        private IRotationSystem RotationSystem { get; }

        public Field Field { get; }
        public Mino CurrentMino { get; private set; }
        public Position CurrentMinoCenterPosition { get; private set; }
        public Position[] CurrentMinoAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, CurrentMinoCenterPosition, RotationDirection.None); }
        public Position[] GhostPieceAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None); }
        public Mino? HeldMino { get; private set; }
        public bool CanHold { get; private set; } = true;
        public Position Origin { get; }

        public Game(GameSettings gameSettings, IGenerator generator, IRotationSystem rotationSystem)
        {
            GameSettings = gameSettings;
            Field = new Field(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue);
            Generator = generator;
            RotationSystem = rotationSystem;
            Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);

            ContinueToNextPiece();
        }

        public MotionResult HardDrop()
        {
            CurrentMinoCenterPosition = GetGhostPieceCenterPosition();
            if (PlaceMino() == MotionResult.SoftDropped) return MotionResult.HardDropped;
            else return MotionResult.Fail;
        }

        public MotionResult PlaceMino()
        {
            object[] values = new object[CurrentMinoAbsolutePositions.Length];
            for (int i = 0; i < values.Length; i++) values[i] = CurrentMino.MinoColor;
            // the Field should ensure that illegal Mino positions aren't possible
            // so we don't need to check to see if the position is legal, it is assumed that it is
            if (Field.FillCells(CurrentMinoAbsolutePositions, values))
            {
                ContinueToNextPiece();
                return MotionResult.SoftDropped;
            }
            else return MotionResult.Fail;
        }

        public MotionResult Hold()
        {
            if (CanHold)
            {
                if (HeldMino is null)
                {
                    HeldMino = CurrentMino;
                    CurrentMino = Generator.Pop();
                }
                else (HeldMino, CurrentMino) = (CurrentMino, HeldMino);
                CanHold = false;
                return MotionResult.Held;
            }
            else return MotionResult.Fail;
        }

        public MotionResult MoveMino(MoveDirection moveDirection)
        {
            Position newPosition;
            MotionResult motionResult;
            switch (moveDirection)
            {
                case MoveDirection.None:
                    return MotionResult.None;
                case MoveDirection.Left:
                    newPosition = CurrentMinoCenterPosition.Add(-1, 0);
                    motionResult = MotionResult.Moved;
                    break;
                case MoveDirection.Right:
                    newPosition = CurrentMinoCenterPosition.Add(1, 0);
                    motionResult = MotionResult.Moved;
                    break;
                case MoveDirection.Down:
                    newPosition = CurrentMinoCenterPosition.Add(0, -1);
                    motionResult = MotionResult.Moved;
                    break;
                case MoveDirection.ClockwiseRotation:
                    newPosition = RotationSystem.Rotate(Field, CurrentMino, RotationDirection.Clockwise, CurrentMinoCenterPosition);
                    motionResult = MotionResult.Rotated;
                    break;
                case MoveDirection.CounterClockwiseRotation:
                    newPosition = RotationSystem.Rotate(Field, CurrentMino, RotationDirection.CounterClockwise, CurrentMinoCenterPosition);
                    motionResult = MotionResult.Rotated;
                    break;
                default:
                    return MotionResult.Fail;
            }

            if (Field.CanFillCells(GetMinoAbsolutePositions(CurrentMino, newPosition, RotationDirection.None)))
            {
                // order matters here, the remove/add ghost piece method is removing/adding the ghost piece based on the piece's current position
                Field.UnfillCells(CurrentMinoAbsolutePositions);
                RemoveGhostPiece();
                CurrentMinoCenterPosition = newPosition;
                Field.FillCells(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
                AddGhostPiece();
                return motionResult;
            }
            return MotionResult.Fail;
        }

        private Position[] GetMinoAbsolutePositions(Mino mino, Position centerPosition, RotationDirection rotationDirection) => mino.GetRotationPositions(rotationDirection).Select(rp => rp + centerPosition).ToArray();

        private Position GetGhostPieceCenterPosition()
        {
            Position ghostPieceCenterPosition = CurrentMinoCenterPosition;
            while (Field.CanFillCells(GetMinoAbsolutePositions(CurrentMino, ghostPieceCenterPosition.Add(0, -1), RotationDirection.None))) ghostPieceCenterPosition = ghostPieceCenterPosition.Add(0, -1);
            return ghostPieceCenterPosition;
        }

        private void RemoveGhostPiece()
        {
            foreach(Position position in GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None))
            {
                Cell cell = Field[position.X, position.Y];
                cell.SetValue(Field.UnfilledCellValue);
            }
        }

        private void AddGhostPiece()
        {
            foreach (Position position in GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None))
            {
                Cell cell = Field[position.X, position.Y];
                cell.SetValue(GameSettings.GhostPieceValue);
            }
        }

        private void ContinueToNextPiece()
        {
            CanHold = true;
            CurrentMino = Generator.Pop();
            CurrentMinoCenterPosition = Origin;
        }
    }
}
