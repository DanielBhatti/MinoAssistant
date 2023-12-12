using MinoAssistant.Game;
using MinoAssistant.Game.Block;
using MinoAssistant.Game.Motion;
using MinoAssistant.Game.Motion.Rotation;
using ReactiveUI;
using System.Collections.Generic;
using System.Threading;

namespace MinoAssistant.UI;

public class GameViewModel : ViewModelBase
{
    private GameSystem Game { get; }
    private Timer Timer { get; }

    public int Width => Game.Width;
    public int Height => Game.Height;
    public Mino CurrentMino => Game.CurrentMino;
    public Mino? HeldMino => Game.HeldMino;

    // Collection of a Collection because we'll want to bind to an ItemsControl twice
    // Can't do that with a multi-array
    public List<List<CellViewModel>> CellViewModels { get; }

    public GameViewModel(GameSystem game)
    {
        Game = game;
        Timer = new Timer(TimerTick, null, 100, 100);

        CellViewModels = new();
        for(var i = 0; i < Game.Width; i++)
        {
            CellViewModels.Add(new List<CellViewModel>());
            for(var j = 0; j < Game.Height; j++) CellViewModels[i].Add(new CellViewModel(Game[i, j]));
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
        //Game.HardDrop();
        RaisePropertyChangedAllCells();
    }

    private void Move(MoveDirection moveDirection)
    {
        //Game.MoveMino(moveDirection);
        RaisePropertyChangedAllCells();
    }

    private void Rotate(RotationDirection rotationDirection)
    {
        //Game.Rotate(rotationDirection);
        RaisePropertyChangedAllCells();
    }

    private void TimerTick(object? sender) => RaisePropertyChangedAllCells();

    private void InitializeGame() => RaisePropertyChangedAllCells();

    private void RaisePropertyChangedAllCells()
    {
        for(var i = 0; i < Game.Width; i++)
        {
            for(var j = 0; j < Game.Height; j++)
            {
                var cellViewModel = CellViewModels[i][j];
                cellViewModel.RaisePropertyChanged(nameof(cellViewModel.Value));
            }
        }
    }

    private void RaisePropertyChangedCells(Position[] positions)
    {
        foreach(var position in positions)
        {
            var cellViewModel = CellViewModels[position.X][position.Y];
            cellViewModel.RaisePropertyChanged(nameof(cellViewModel.Value));
        }
    }
}
