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

        public void UnFill(object value)
        {
            Cell.Unfill(value);
            this.RaisePropertyChanged(nameof(Cell));
        }

        public void SetValue(object value)
        {
            Cell.SetValue(value);
            this.RaisePropertyChanged(nameof(Cell));
        }
    }
}
