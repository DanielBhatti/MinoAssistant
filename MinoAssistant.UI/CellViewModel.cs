using MinoAssistant.Board;
using ReactiveUI;

namespace MinoAssistant.UI
{
    public class CellViewModel : ViewModelBase
    {
        public Cell Cell { get; }

        public CellViewModel(Cell cell) => Cell = cell;

        public void Fill(object value)
        {
            Cell.Fill(value);
            this.RaisePropertyChanged(nameof(Cell));
        }

        public void UnFill()
        {
            Cell.Unfill();
            this.RaisePropertyChanged(nameof(Cell));
        }
    }
}
