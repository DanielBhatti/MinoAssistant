using MinoAssistant.Board.Block;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion;
using MinoAssistant.Board.Motion.Rotation;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace MinoAssistant.Board
{
    public class Game
    {
        private GameSettings GameSettings { get; }
        private IGenerator Generator { get; }
        private IRotationSystem RotationSystem { get; }

        private Position CurrentMinoCenterPosition { get; set; }
        private Position Origin { get; }
        private Stopwatch LockStopwatch { get; }
        private Timer LockTimer { get; }
        private Timer GravityTimer { get; }

        public Field Field { get; }
        public Mino CurrentMino { get; private set; }
        public Position[] CurrentMinoAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, CurrentMinoCenterPosition, RotationDirection.None); }
        public Position[] GhostPieceAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None); }
        public Mino? HeldMino { get; private set; }
        public bool CanHold { get; private set; } = true;
        public GameState GameState { get; private set; }

        public Game(GameSettings gameSettings, IGenerator generator, IRotationSystem rotationSystem)
        {
            GameSettings = gameSettings;
            Field = new Field(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue);
            Generator = generator;
            RotationSystem = rotationSystem;
            Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);
            GravityTimer = new Timer(GravityTimerTick, null, (int)gameSettings.Gravity, (int)gameSettings.Gravity);
            LockStopwatch = new Stopwatch();
            //LockTimer = new Timer(LockTimerTick, null, (int)(gameSettings.LockDelaySeconds / 2), (int)(gameSettings.LockDelaySeconds / 2));
            GameState = GameState.Playing;

            ContinueToNextPiece();
        }

        public MotionType HardDrop()
        {
            Field.SetCellValues(CurrentMinoAbsolutePositions, GameSettings.UnfilledCellValue);
            CurrentMinoCenterPosition = GetGhostPieceCenterPosition();
            if (PlaceMino() == MotionType.SoftDropped) return MotionType.HardDropped;
            else return MotionType.Fail;
        }

        public MotionType PlaceMino()
        {
            if (Field.FillCells(CurrentMinoAbsolutePositions, CurrentMino.MinoColor))
            {
                ContinueToNextPiece();
                return MotionType.SoftDropped;
            }
            else return MotionType.Fail;
        }

        public MotionType Hold()
        {
            Field.SetCellValues(CurrentMinoAbsolutePositions, GameSettings.UnfilledCellValue);
            RemoveGhostPiece();

            if (CanHold)
            {
                if (HeldMino is null)
                {
                    HeldMino = CurrentMino;
                    ContinueToNextPiece();
                }
                else
                {
                    (HeldMino, CurrentMino) = (CurrentMino, HeldMino);
                    CanHold = false;
                    CurrentMinoCenterPosition = Origin;
                }

                Field.SetCellValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
                AddGhostPiece();
                return MotionType.Held;

            }
            Field.SetCellValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
            AddGhostPiece();
            return MotionType.Fail;
        }

        public MotionResult MoveMino(MoveDirection moveDirection)
        {
            Field.SetCellValues(CurrentMinoAbsolutePositions, GameSettings.UnfilledCellValue);
            RemoveGhostPiece();
            MotionResult motionResult;
            switch (moveDirection)
            {
                case MoveDirection.None:
                    motionResult = new MotionResult(CurrentMino, MotionType.None, CurrentMinoCenterPosition, CurrentMinoCenterPosition);
                    break;
                case MoveDirection.Left:
                    motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(-1, 0));
                    break;
                case MoveDirection.Right:
                    motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(1, 0));
                    break;
                case MoveDirection.Down:
                    motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(0, -1));
                    break;
                case MoveDirection.ClockwiseRotation:
                    motionResult = RotationSystem.Rotate(Field, CurrentMino, RotationDirection.Clockwise, CurrentMinoCenterPosition);
                    break;
                case MoveDirection.CounterClockwiseRotation:
                    motionResult = RotationSystem.Rotate(Field, CurrentMino, RotationDirection.CounterClockwise, CurrentMinoCenterPosition);
                    break;
                default:
                    motionResult = new MotionResult(CurrentMino, MotionType.Fail, CurrentMinoCenterPosition, CurrentMinoCenterPosition);
                    break;
            }

            RotationDirection rotationDirection;
            switch (moveDirection)
            {
                case MoveDirection.Left:
                case MoveDirection.Right:
                case MoveDirection.Down:
                    rotationDirection = RotationDirection.None;
                    break;
                case MoveDirection.ClockwiseRotation:
                    rotationDirection = RotationDirection.Clockwise;
                    break;
                case MoveDirection.CounterClockwiseRotation:
                    rotationDirection = RotationDirection.CounterClockwise;
                    break;
                default:
                    rotationDirection = RotationDirection.None;
                    break;
            }

            if (Field.CanFillCells(GetMinoAbsolutePositions(CurrentMino, motionResult.NewCenterPosition, rotationDirection)))
            {
                CurrentMinoCenterPosition = motionResult.NewCenterPosition;
                CurrentMino.Rotate(rotationDirection);
            }

            Field.SetCellValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
            AddGhostPiece();
            return motionResult;
        }

        public void Pause()
        {
            GameState = GameState.Paused;
        }

        public void Unpause()
        {
            GameState = GameState.Playing;
        }

        private void GravityTimerTick(object? sender) => MoveMino(MoveDirection.Down);

        private void LockTimerTick(object? sender)
        {
            MotionResult motionResult = new MotionResult(CurrentMino, MotionType.Moved, CurrentMinoCenterPosition, CurrentMinoCenterPosition.Add(0, -1));
            if (motionResult.MotionType == MotionType.Fail || motionResult.MotionType == MotionType.None)
            {
                if (!LockStopwatch.IsRunning)
                {
                    LockStopwatch.Start();
                }
                if (LockStopwatch.ElapsedMilliseconds > 1000 * GameSettings.LockDelaySeconds) PlaceMino();
            }
            else LockStopwatch.Reset();
        }

        private bool IsLineFilled(int rowIndex)
        {
            for (int i = 0; i < Field.Width; i++) if (Field.IsFilledCell(new Position(i, rowIndex))) return false;
            return true;
        }

        private bool ClearLine(int rowIndex)
        {
            if (!IsLineFilled(rowIndex)) return false;
            for (int j = 0; j < Field.Width; j++) Field.UnfillCell(new Position(j, rowIndex), GameSettings.UnfilledCellValue);
            for (int i = rowIndex + 1; i < Field.Height; i++)
            {
                for (int j = 0; j < Field.Width; j++)
                {
                    Position currentPosition = new Position(i, j);
                    Position belowPosition = new Position(i - 1, j);
                    if (Field.IsFilledCell(currentPosition))
                    {
                        ReadOnlyCell cell = Field[currentPosition.X, currentPosition.Y];
                        MinoColor value = cell.Value;
                        Field.UnfillCell(currentPosition, GameSettings.UnfilledCellValue);
                        Field.FillCell(belowPosition, value);
                    }
                }
            }
            return true;
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
            foreach(Position position in GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None)) Field.SetCellValue(position, GameSettings.UnfilledCellValue);
        }

        private void AddGhostPiece()
        {
            foreach (Position position in GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None)) Field.SetCellValue(position, GameSettings.GhostPieceValue);
        }

        private void ContinueToNextPiece()
        {
            CanHold = true;
            CurrentMino = Generator.Pop();
            CurrentMinoCenterPosition = Origin;

            if (!Field.CanFillCells(CurrentMinoAbsolutePositions))
            {
                GameState = GameState.GameOver;
                return;
            }

            Field.SetCellValues(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
            Field.SetCellValues(GhostPieceAbsolutePositions, GameSettings.GhostPieceValue);
        }
    }
}
