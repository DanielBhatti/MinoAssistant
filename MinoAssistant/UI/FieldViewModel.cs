using MinoAssistant.Board;
using System.Collections.Generic;

namespace MinoAssistant.UI
{
    public class FieldViewModel : ViewModelBase
    {
        // Collection of a Collection because we'll want to bind to an ItemsControl twice
        // Can't do that with a multi-array
        public List<List<CellViewModel>> Cells { get; }

        public FieldViewModel(Field field)
        {
            Cells = new();
            for(int i = 0; i < field.Width; i++)
            {
                Cells.Add(new List<CellViewModel>());
                for (int j = 0; j < field.Height; j++)
                {
                    Cells[i].Add(new CellViewModel(field[i, j]));
                }
            }
        }
    }
}
