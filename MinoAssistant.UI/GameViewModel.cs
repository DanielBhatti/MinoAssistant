using MinoAssistant.Board;
using System.Threading;
using ReactiveUI;
using System.Collections.Generic;
using MinoAssistant.Board.Block;
using MinoAssistant.Board.Motion;
using MinoAssistant.Board.Motion.Rotation;

namespace MinoAssistant.UI
{
    public class GameViewModel : ViewModelBase
    {
        private GameController Game { get; }
        private Timer Timer { get; }

        public int Width { get => Game.Field.Width; }
        public int Height { get => Game.Field.Height; }
        public Mino CurrentMino { get => Game.CurrentMino; }
        public Mino? HeldMino { get => Game.HeldMino; }

        // Collection of a Collection because we'll want to bind to an ItemsControl twice
        // Can't do that with a multi-array
        public List<List<CellViewModel>> CellViewModels { get; }

        public GameViewModel(GameController game)
        {
            Game = game;
            Timer = new Timer(TimerTick, null, 100, 100);

            CellViewModels = new();
            for (int i = 0; i < Game.Field.Width; i++)
            {
                CellViewModels.Add(new List<CellViewModel>());
                for (int j = 0; j < Game.Field.Height; j++) CellViewModels[i].Add(new CellViewModel(Game.Field[i, j]));
            }

            InitializeGame();
        }

        public void MoveLeftCommand() => Move(MoveDirection.Left);

        public void MoveRightCommand() => Move(MoveDirection.Right);

        public void MoveDownCommand() => Move(MoveDirection.Down);

        public void RotateCounterClockwiseCommand() => Rotate(RotationDirection.CounterClockwise);

        public void RotateClockwiseCommand() => Rotate(RotationDirection.Clockwise);

        public void HoldCommand()
        {
            Game.Hold();
            RaisePropertyChangedAllCells();
        }

        public void HardDropCommand()
        {
            Game.HardDrop();
            RaisePropertyChangedAllCells();
        }

        private void Move(MoveDirection moveDirection)
        {
            Game.MoveMino(moveDirection);
            RaisePropertyChangedAllCells();
        }

        private void Rotate(RotationDirection rotationDirection)
        {
            Game.Rotate(rotationDirection);
            RaisePropertyChangedAllCells();
        }

        private void TimerTick(object? sender)
        {
            RaisePropertyChangedAllCells();
        }

        private void InitializeGame() => RaisePropertyChangedAllCells();

        private void RaisePropertyChangedAllCells()
        {
            for (int i = 0; i < Game.Field.Width; i++)
            {
                for (int j = 0; j < Game.Field.Height; j++)
                {
                    CellViewModel cellViewModel = CellViewModels[i][j];
                    cellViewModel.RaisePropertyChanged(nameof(cellViewModel.Value));
                }
            }
        }

        private void RaisePropertyChangedCells(Position[] positions)
        {
            foreach (Position position in positions)
            {
                CellViewModel cellViewModel = CellViewModels[position.X][position.Y];
                cellViewModel.RaisePropertyChanged(nameof(cellViewModel.Value));
            }
        }
    }
}
