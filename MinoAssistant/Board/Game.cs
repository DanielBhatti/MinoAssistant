using MinoAssistant.Board.Minos;

namespace MinoAssistant.Board
{
    public class Game
    {
        private GameSettings GameSettings { get; }
        private Field Field { get; }
        private IGenerator Generator { get; }

        public Cell[,] Cells { get => Field.Cells; }
        public Mino CurrentMino { get; private set; }
        public Position CurrentMinoPosition { get; private set; }
        public Mino? HeldMino { get; private set; }
        public bool CanHold { get; private set; } = true;

        public Game(GameSettings gameSettings, int fieldWidth, int FieldHeight, IGenerator generator)
        {
            GameSettings = gameSettings;
            Field = new Field(fieldWidth, FieldHeight);
            Generator = generator;
            CurrentMino = Generator.Pop();
        }

        public void HardDrop()
        {
            Position previousPosition = CurrentMinoPosition;
            MoveDown();
            while (!previousPosition.Equals(CurrentMinoPosition)) MoveDown();
            PlaceMino();
        }

        public void PlaceMino()
        {
            Field.SetMinoPosition(CurrentMino, CurrentMinoPosition);
            CanHold = true;
        }

        public void MoveDown() => MoveMino(Field, CurrentMino, CurrentMinoPosition, 0, -1, RotationType.None);

        public void MoveLeft() => MoveMino(Field, CurrentMino, CurrentMinoPosition, -1, 0, RotationType.None);

        public void MoveRight() => MoveMino(Field, CurrentMino, CurrentMinoPosition, -1, 0, RotationType.None);

        public void RotateClockwise() => MoveMino(Field, CurrentMino, CurrentMinoPosition, 0, 0, RotationType.Clockwise);

        public void RotateCounterClockwise() => MoveMino(Field, CurrentMino, CurrentMinoPosition, 0, 0, RotationType.CounterClockwise);

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

        private void MoveMino(Field field, Mino mino, Position currentPosition, int xMove, int yMove, RotationType rotationType)
        {
            switch (rotationType)
            {
                case RotationType.None:
                    Position newPosition = currentPosition + Position.Create(xMove, yMove);
                    if (Field.CanSetMinoPosition(mino, newPosition)) field.SetMinoPosition(mino, newPosition);
                    break;
                case RotationType.Clockwise:
                    break;
                case RotationType.CounterClockwise:
                    break;
            }
        }
    }
}
