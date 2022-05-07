using System.Linq;

namespace MinoAssistant.Board
{
    public class Game
    {
        private GameSettings GameSettings { get; }
        private Field Field { get; }
        private IGenerator Generator { get; }
        private IRotationSystem RotationSystem { get; }

        public Cell[,] Cells { get => Field.Cells; }
        public Mino CurrentMino { get; private set; }
        public Position CurrentMinoCenterPosition { get; private set; }
        public Position[] CurrentMinoAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, CurrentMinoCenterPosition, RotationDirection.None); }
        public Mino? HeldMino { get; private set; }
        public bool CanHold { get; private set; } = true;
        public Position Origin { get => new Position(5, Field.Height); }

        public Game(GameSettings gameSettings, int fieldWidth, int FieldHeight, IGenerator generator, IRotationSystem rotationSystem)
        {
            GameSettings = gameSettings;
            Field = new Field(fieldWidth, FieldHeight);
            Generator = generator;
            CurrentMino = Generator.Pop();
            RotationSystem = rotationSystem;
        }

        public Position GetGhostPieceCenterPosition()
        {
            Position ghostPieceCenterPosition = CurrentMinoCenterPosition;
            while (Field.CanSetPositions(GetMinoAbsolutePositions(CurrentMino, ghostPieceCenterPosition.Add(0, -1), RotationDirection.None))) ghostPieceCenterPosition = ghostPieceCenterPosition.Add(0, -1);
            return ghostPieceCenterPosition;
        }

        public MotionResult HardDrop()
        {
            CurrentMinoCenterPosition = GetGhostPieceCenterPosition();
            if (PlaceMino() == MotionResult.SoftDropped) return MotionResult.HardDropped;
            else return MotionResult.Fail;
        }

        public MotionResult PlaceMino(object[]? values = null)
        {
            // the Field should ensure that illegal Mino positions aren't possible
            // so we don't need to check to see if the position is legal, it is assumed that it is
            if (Field.SetPositions(CurrentMinoAbsolutePositions, values))
            {
                CanHold = true;
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
            switch (moveDirection)
            {
                case MoveDirection.None:
                    return MotionResult.None;
                case MoveDirection.Left:
                    newPosition = CurrentMinoCenterPosition.Add(-1, 0);
                    if (Field.CanSetPositions(GetMinoAbsolutePositions(CurrentMino, newPosition, RotationDirection.None))) CurrentMinoCenterPosition = newPosition;
                    return MotionResult.Moved;
                case MoveDirection.Right:
                    newPosition = CurrentMinoCenterPosition.Add(1, 0);
                    if (Field.CanSetPositions(GetMinoAbsolutePositions(CurrentMino, newPosition, RotationDirection.None))) CurrentMinoCenterPosition = newPosition;
                    return MotionResult.Moved;
                case MoveDirection.Down:
                    newPosition = CurrentMinoCenterPosition.Add(0, -1);
                    if (Field.CanSetPositions(GetMinoAbsolutePositions(CurrentMino, newPosition, RotationDirection.None))) CurrentMinoCenterPosition = newPosition;
                    return MotionResult.Moved;
            }
            return MotionResult.Fail;
        }

        public MotionResult RotateMino(RotationDirection rotationDirection) => RotationSystem.Rotate(Field, CurrentMino, rotationDirection);

        private Position[] GetMinoAbsolutePositions(Mino mino, Position centerPosition, RotationDirection rotationDirection) => mino.GetRotationPositions(rotationDirection).Select(rp => rp + centerPosition).ToArray();
    }
}
