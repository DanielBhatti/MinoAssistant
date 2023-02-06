using MinoAssistant.Board;

namespace MinoAssistant.UI
{
    public class MainWindowViewModel : ViewModelBase
    {
        public GameViewModel GameViewModel { get; }

        public MainWindowViewModel() { } // get rid of this later, need to figure out dependency injection
        public MainWindowViewModel(GameController game)
        {
            GameViewModel = new GameViewModel(game);
        }
    }
}
