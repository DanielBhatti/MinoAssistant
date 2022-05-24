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
        private Timer LockTimer { get; }
        private Timer GravityTimer { get; }

        public Field Field { get; }
        public Mino CurrentMino { get; private set; }
        public Position[] CurrentMinoAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, CurrentMinoCenterPosition, RotationDirection.None); }
        public Position[] GhostPieceAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, GetGhostPieceCenterPosition(), RotationDirection.None); }
        public Mino? HeldMino { get; private set; }
        public bool CanHold { get; private set; } = true;

        public Game(GameSettings gameSettings, IGenerator generator, IRotationSystem rotationSystem)
        {
            GameSettings = gameSettings;
            Field = new Field(gameSettings.FieldWidth, gameSettings.FieldHeight, gameSettings.UnfilledCellValue);
            Generator = generator;
            RotationSystem = rotationSystem;
            Origin = new Position(GameSettings.OriginX, GameSettings.OriginY);
            GravityTimer = new Timer(GravityTimerTick, null, (int)gameSettings.Gravity, (int)gameSettings.Gravity);

            ContinueToNextPiece();
        }

        public MotionType HardDrop()
        {
            CurrentMinoCenterPosition = GetGhostPieceCenterPosition();
            if (PlaceMino() == MotionType.SoftDropped) return MotionType.HardDropped;
            else return MotionType.Fail;
        }

        public MotionType PlaceMino()
        {
            object[] values = new object[CurrentMinoAbsolutePositions.Length];
            for (int i = 0; i < values.Length; i++) values[i] = CurrentMino.MinoColor;
            // the Field should ensure that illegal Mino positions aren't possible
            // so we don't need to check to see if the position is legal, it is assumed that it is
            if (Field.FillCells(CurrentMinoAbsolutePositions, values))
            {
                ContinueToNextPiece();
                return MotionType.SoftDropped;
            }
            else return MotionType.Fail;
        }

        public MotionType Hold()
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
                return MotionType.Held;
            }
            else return MotionType.Fail;
        }

        public MotionResult MoveMino(MoveDirection moveDirection)
        {
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

            if (Field.CanFillCells(GetMinoAbsolutePositions(CurrentMino, motionResult.NewCenterPosition, RotationDirection.None)))
            {
                // order matters here, the remove/add ghost piece method is removing/adding the ghost piece based on the piece's current position
                Field.UnfillCells(CurrentMinoAbsolutePositions);
                RemoveGhostPiece();
                CurrentMinoCenterPosition = motionResult.NewCenterPosition;
                Field.FillCells(CurrentMinoAbsolutePositions, CurrentMino.MinoColor);
                AddGhostPiece();
                return motionResult;
            }

            if (motionResult.MotionType != MotionType.None) ResetLockTimer();
            return motionResult;
        }

        public void Pause()
        {

        }

        public void Unpause()
        {

        }

        private void GravityTimerTick(object? sender) => MoveMino(MoveDirection.Down);

        private void ResetLockTimer()
        {
            
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
