using MinoAssistant.Board;
using MinoAssistant.Board.Block;

namespace MinoAssistant.UI
{
    public class CellViewModel : ViewModelBase
    {
        private ReadOnlyCell Cell { get; }
        public MinoColor Value { get => Cell.Value; }

        public CellViewModel(ReadOnlyCell cell) => Cell = cell;
    }
}
