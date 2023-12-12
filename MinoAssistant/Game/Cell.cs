using MinoAssistant.Game.Block;

namespace MinoAssistant.Game;

public class Cell
{
    public MinoColor Value { get; private set; }

    public Cell(MinoColor value) => Value = value;

    public void Set(MinoColor value) => Value = value;
}
