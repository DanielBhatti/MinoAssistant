using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MinoAssistant.Board;

namespace MinoAssistant.UI
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                GameSettings gameSettings = new GameSettings();
                IGenerator generator = new BpsGenerator();
                IRotationSystem rotationSystem = new SrsRotationSystem();
                Game game = new Game(gameSettings, 10, 20, generator, rotationSystem);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(game),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
