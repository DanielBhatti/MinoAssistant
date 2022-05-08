using MinoAssistant.Board;
using ReactiveUI;

namespace MinoAssistant.UI
{
    public class CellViewModel : ViewModelBase
    {
        private Cell _cell = null!;
        public Cell Cell
        {
            get => _cell;
            set => this.RaiseAndSetIfChanged(ref value, _cell);
        }

        public CellViewModel(Cell cell) => Cell = cell;
    }
}
