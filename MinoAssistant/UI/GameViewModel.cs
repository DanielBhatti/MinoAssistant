using MinoAssistant.Board;
using MinoAssistant.Board.Minos;

namespace MinoAssistant.UI
{
    public class GameViewModel : ViewModelBase
    {
        private Game Game { get; }

        public int Width { get => Game.Cells.GetLength(0); }
        public int Height { get => Game.Cells.GetLength(1); }
        public Cell[,] Cells { get => Game.Cells; }
        public Mino CurrentMino { get => Game.CurrentMino; }
        public Mino? HeldMino { get => Game.HeldMino; }

        public GameViewModel(Game game) => Game = game;

        public void Move(MoveDirection moveType, RotationDirection rotationType)
        {
            switch (moveType)
            {
                case MoveDirection.None:
                    switch (rotationType)
                    {
                        case RotationDirection.None:
                            break;
                        case RotationDirection.Clockwise:
                            break;
                        case RotationDirection.CounterClockwise:
                            break;
                    }
                    break;
                case MoveDirection.Left:
                    switch (rotationType)
                    {
                        case RotationDirection.None:
                            break;
                        case RotationDirection.Clockwise:
                            break;
                        case RotationDirection.CounterClockwise:
                            break;
                    }
                    break;
                case MoveDirection.Right:
                    switch (rotationType)
                    {
                        case RotationDirection.None:
                            break;
                        case RotationDirection.Clockwise:
                            break;
                        case RotationDirection.CounterClockwise:
                            break;
                    }
                    break;
                case MoveDirection.Down:
                    switch (rotationType)
                    {
                        case RotationDirection.None:
                            break;
                        case RotationDirection.Clockwise:
                            break;
                        case RotationDirection.CounterClockwise:
                            break;
                    }
                    break;
            }
        }

        private void ResetLockTimer()
        {

        }
    }
}
