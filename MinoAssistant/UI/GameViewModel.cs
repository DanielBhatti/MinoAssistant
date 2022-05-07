using MinoAssistant.Board;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using ReactiveUI;

namespace MinoAssistant.UI
{
    public class GameViewModel : ViewModelBase
    {
        private Game Game { get; }

        public Timer Timer { get; }

        public int Width { get => Game.Cells.GetLength(0); }
        public int Height { get => Game.Cells.GetLength(1); }
        private List<List<Cell>> _cells;
        public List<List<Cell>> Cells
        {
            get => _cells;
            set => this.RaiseAndSetIfChanged(ref _cells, value);
        }
        public Mino CurrentMino { get => Game.CurrentMino; }
        public Mino? HeldMino { get => Game.HeldMino; }

        public GameViewModel(Game game)
        {
            Game = game;
            Timer = new Timer(TimerTick, null, 1000, 1000);
        }

        public void Move(MoveDirection moveDirection)
        {
            Game.MoveMino(moveDirection);
            ResetLockTimer();
        }

        public void Rotate(RotationDirection rotationDirection)
        {
            Game.RotateMino(rotationDirection);
            ResetLockTimer();
        }

        private void ResetLockTimer()
        {

        }

        private void TimerTick(object? sender)
        {
            Move(MoveDirection.Down);
        }
    }
}
