using MinoAssistant.Game.Block;
using MinoAssistant.Game.Motion;
using MinoAssistant.Game.Motion.Rotation;
using System.Linq;

namespace MinoAssistant.Game;

public class BasicGameController
{
    private GameSystem GameSystem { get; }

    public BasicGameController(GameSystem gameSystem) => GameSystem = gameSystem;

    public MotionType Move(MoveDirection moveDirection) => GameSystem.Move(moveDirection);
    public MotionType Rotate(RotationDirection rotationDirection) => GameSystem.Rotate(rotationDirection.ToRotationAmount());
    public MotionType Hold() => GameSystem.Hold();
}
