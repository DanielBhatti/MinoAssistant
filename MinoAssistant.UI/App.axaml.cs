using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MinoAssistant.Board;
using MinoAssistant.UI;
using System.Collections.Generic;

namespace MinoAssistant.UI
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                GameSettings gameSettings = new GameSettings(@"C:\Users\bhatt\Repositories\MinoAssistant\MinoAssistant.UI\Assets\white-block.png", "");
                IGenerator generator = new BpsGenerator();
                IRotationSystem rotationSystem = new SrsRotationSystem();
                Game game = new Game(gameSettings, generator, rotationSystem);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(game),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
