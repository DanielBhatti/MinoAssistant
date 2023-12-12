using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using MinoAssistant.Game;
using MinoAssistant.Game.Generator;
using MinoAssistant.Game.Motion.Rotation;

namespace MinoAssistant.UI;

public partial class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if(ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var gameSettings = GameSettings.DefaultSettings;
            MinoGenerator generator = new BpsGenerator();
            RotationSystem rotationSystem = new ClassicRotationSystem();
            var game = new GameSystem(gameSettings, generator, rotationSystem);

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(game),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
