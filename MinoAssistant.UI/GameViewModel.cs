using MinoAssistant.Board;
using System.Threading;
using System.Threading.Tasks;
using ReactiveUI;
using System.Collections.Generic;

namespace MinoAssistant.UI
{
    public class GameViewModel : ViewModelBase
    {
        private Game Game { get; }
        private Timer Timer { get; }

        public int Width { get => Game.Field.Width; }
        public int Height { get => Game.Field.Height; }
        public Mino CurrentMino { get => Game.CurrentMino; }
        public Mino? HeldMino { get => Game.HeldMino; }

        // Collection of a Collection because we'll want to bind to an ItemsControl twice
        // Can't do that with a multi-array
        public List<List<CellViewModel>> CellViewModels { get; }

        public GameViewModel(Game game)
        {
            Game = game;
            Timer = new Timer(TimerTick, null, 1000, 1000);

            CellViewModels = new();
            for (int i = 0; i < Game.Field.Width; i++)
            {
                CellViewModels.Add(new List<CellViewModel>());
                for (int j = Game.Field.Height - 1; j >= 0; j--)
                {
                    CellViewModels[i].Add(new CellViewModel(Game.Field[i, j]));
                }
            }

            InitializeGame();
        }

        public void Move(MoveDirection moveDirection)
        {
            Position[] previousPositions = Game.CurrentMinoAbsolutePositions;
            Game.MoveMino(moveDirection);
            Position[] newPositions = Game.CurrentMinoAbsolutePositions;
            RaisePropertyChangedAllCells();
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

        private async void TimerTick(object? sender)
        {
            Move(MoveDirection.Down);
            await Task.Run(() => this.RaisePropertyChanged(nameof(GameViewModel)));
        }

        private void InitializeGame()
        {
            for (int i = 0; i < Game.Field.Width; i++) for (int j = 0; j < Game.Field.Height; j++) CellViewModels[i][j].Cell.SetValue(@"C:\Users\bhatt\Repositories\MinoAssistant\MinoAssistant.UI\Assets\white-block.png");
            RaisePropertyChangedAllCells();
            foreach (Position p in Game.CurrentMinoAbsolutePositions) CellViewModels[p.X][p.Y].SetValue(@"C:\Users\bhatt\Repositories\MinoAssistant\MinoAssistant.UI\Assets\red-block.png");
            RaisePropertyChangedCells(Game.CurrentMinoAbsolutePositions);
        }

        private void RaisePropertyChangedAllCells()
        {
            for (int i = 0; i < Game.Field.Width; i++)
            {
                for (int j = 0; j < Game.Field.Height; j++)
                {
                    CellViewModel cellViewModel = CellViewModels[i][j];
                    cellViewModel.RaisePropertyChanged(nameof(cellViewModel.Cell));
                }
            }
        }

        private void RaisePropertyChangedCells(Position[] positions)
        {
            foreach (Position position in positions)
            {
                CellViewModel cellViewModel = CellViewModels[position.X][position.Y];
                cellViewModel.RaisePropertyChanged(nameof(cellViewModel.Cell));
            }
        }
    }
}
