using MinoAssistant.Board.Minos;
using System.Linq;

namespace MinoAssistant.Board
{
    public class Game
    {
        private GameSettings GameSettings { get; }
        private Field Field { get; }
        private IGenerator Generator { get; }

        public Cell[,] Cells { get => Field.Cells; }
        public Mino CurrentMino { get; private set; }
        public Position CurrentMinoCenterPosition { get; private set; }
        public Position[] CurrentMinoAbsolutePositions { get => GetMinoAbsolutePositions(CurrentMino, CurrentMinoCenterPosition, RotationDirection.None); }
        public Mino? HeldMino { get; private set; }
        public bool CanHold { get; private set; } = true;

        public Game(GameSettings gameSettings, int fieldWidth, int FieldHeight, IGenerator generator)
        {
            GameSettings = gameSettings;
            Field = new Field(fieldWidth, FieldHeight);
            Generator = generator;
            CurrentMino = Generator.Pop();
        }

        public Position GetGhostPieceCenterPosition()
        {
            Position ghostPieceCenterPosition = CurrentMinoCenterPosition;
            Position[] ghostPieceAbsolutePositions = GetMinoAbsolutePositions(CurrentMino, ghostPieceCenterPosition, RotationDirection.None);
            while (Field.CanSetPositions(ghostPieceAbsolutePositions))
            {
                ghostPieceCenterPosition = ghostPieceCenterPosition.Add(0, -1);
            }
            return ghostPieceCenterPosition;
        }

        public void HardDrop()
        {
            CurrentMinoCenterPosition = GetGhostPieceCenterPosition();
            PlaceMino();
        }

        public void PlaceMino(object[]? values = null)
        {
            // the Field should ensure that illegal Mino positions aren't possible
            // so we don't need to check to see if the position is legal
            Field.SetPositions(CurrentMinoAbsolutePositions, values);
            CanHold = true;
        }

        public void Hold()
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
            }
        }

        public bool MoveMino(MoveDirection moveDirection, RotationDirection rotationDirection) => MoveMino(Field, CurrentMino, CurrentMinoCenterPosition, moveDirection, rotationDirection);

        private bool MoveMino(Field field, Mino mino, Position currentPosition, MoveDirection moveDirection, RotationDirection rotationDirection)
        {
            Position newPosition;
            switch (rotationDirection)
            {
                case RotationDirection.None:
                    switch (moveDirection)
                    {
                        case MoveDirection.None:
                            return false;
                        case MoveDirection.Left:
                            newPosition = currentPosition.Add(-1, 0);
                            if (Field.CanSetPositions(GetMinoAbsolutePositions(mino, newPosition, rotationDirection))) CurrentMinoCenterPosition = newPosition; 
                            return true;
                        case MoveDirection.Right:
                            newPosition = currentPosition.Add(1, 0);
                            if (Field.CanSetPositions(GetMinoAbsolutePositions(mino, newPosition, rotationDirection))) CurrentMinoCenterPosition = newPosition;
                            return true;
                        case MoveDirection.Down:
                            newPosition = currentPosition.Add(0, -1);
                            if (Field.CanSetPositions(GetMinoAbsolutePositions(mino, newPosition, rotationDirection))) CurrentMinoCenterPosition = newPosition;
                            return true;
                    }
                    break;
                case RotationDirection.Clockwise:
                    switch (moveDirection)
                    {
                        case MoveDirection.None:
                            newPosition = currentPosition;
                            return true;
                            break;
                        case MoveDirection.Left:
                            break;
                        case MoveDirection.Right:
                            break;
                        case MoveDirection.Down:
                            break;
                    }
                    break;
                case RotationDirection.CounterClockwise:
                    switch (moveDirection)
                    {
                        case MoveDirection.None:
                            break;
                        case MoveDirection.Left:
                            break;
                        case MoveDirection.Right:
                            break;
                        case MoveDirection.Down:
                            break;
                    }
                    break;
            }
            return false;
        }

        private Position[] GetMinoAbsolutePositions(Mino mino, Position centerPosition, RotationDirection rotationDirection) => mino.GetRotationPositions(rotationDirection).Select(rp => rp + centerPosition).ToArray();
    }
}
