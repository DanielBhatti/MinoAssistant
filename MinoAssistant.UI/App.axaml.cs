using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MinoAssistant.Board;
using MinoAssistant.Board.Generator;
using MinoAssistant.Board.Motion.Rotation;
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
                GameSettings gameSettings = new GameSettings(Board.Block.MinoColor.White, Board.Block.MinoColor.Black);
                IMinoGenerator generator = new BpsGenerator();
                IRotationSystem rotationSystem = new ClassicRotationSystem();
                GameController game = new GameController(gameSettings, generator, rotationSystem);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(game),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
