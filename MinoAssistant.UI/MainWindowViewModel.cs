using MinoAssistant.Board;

namespace MinoAssistant.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GameViewModel GameViewModel { get; }

        public MainWindowViewModel() { }
        public MainWindowViewModel(Game game)
        {
            GameViewModel = new GameViewModel(game);
        }
    }
}
