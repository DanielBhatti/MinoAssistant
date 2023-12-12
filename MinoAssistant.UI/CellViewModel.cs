using MinoAssistant.Game;
using MinoAssistant.Game.Block;

namespace MinoAssistant.UI;

public class CellViewModel : ViewModelBase
{
    private Cell Cell { get; }
    public MinoColor Value => Cell.Value;

    public CellViewModel(Cell cell) => Cell = cell;
}
