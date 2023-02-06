using MinoAssistant.Board;
using MinoAssistant.Board.Block;

namespace MinoAssistant.UI
{
    public class CellViewModel : ViewModelBase
    {
        private Cell Cell { get; }
        public MinoColor Value { get => Cell.Value; }

        public CellViewModel(Cell cell) => Cell = cell;
    }
}
