using MinoAssistant.Board.Block;

namespace MinoAssistant.Board
{
    public class ReadOnlyCell
    {
        private Cell _cell;

        public MinoColor Value { get => _cell.Value; }
        public bool IsFilled { get => _cell.IsFilled; }

        public ReadOnlyCell(Cell cell) => _cell = cell;
    }
}
