using MinoAssistant.Board;
using System;
using System.Collections.Generic;
using System.Text;

namespace MinoAssistant.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GameViewModel GameViewModel { get; }

        public MainWindowViewModel(Game game)
        {
            GameViewModel = new GameViewModel(game);
        }
    }
}
