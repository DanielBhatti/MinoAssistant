using MinoAssistant.Game;

namespace MinoAssistant.UI;

public class MainWindowViewModel : ViewModelBase
{
    public GameViewModel GameViewModel { get; }

    public MainWindowViewModel(GameSystem game) => GameViewModel = new GameViewModel(game);
}
