using MinoAssistant.Board.Block;

namespace MinoAssistant.Board
{
    public class Cell
    {
        public MinoColor Value { get; private set; }

        public Cell(MinoColor value) => Value = value;

        public void Set(MinoColor value) => Value = value;
    }
}
