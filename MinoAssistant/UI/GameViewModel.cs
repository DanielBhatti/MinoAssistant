using MinoAssistant.Board;
using System.Collections.Generic;
using System.Threading;
using ReactiveUI;

namespace MinoAssistant.UI
{
    public class GameViewModel : ViewModelBase
    {
        private Game Game { get; }

        public Timer Timer { get; }

        public FieldViewModel FieldViewModel { get; }
        public int Width { get => Game.Field.Width; }
        public int Height { get => Game.Field.Height; }
        public Mino CurrentMino { get => Game.CurrentMino; }
        public Mino? HeldMino { get => Game.HeldMino; }

        public GameViewModel(Game game)
        {
            Game = game;
            FieldViewModel = new FieldViewModel(Game.Field);
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
