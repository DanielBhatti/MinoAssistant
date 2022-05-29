using MinoAssistant.Board.Block;

namespace MinoAssistant.Board
{
    public class Cell
    {
        public MinoColor Value { get; private set; }
        public bool IsFilled { get; private set; }

        public Cell(MinoColor value, bool isFilled)
        {
            Value = value;
            IsFilled = isFilled;
        }

        public void Fill(MinoColor value)
        {
            IsFilled = true;
            Value = value;
        }

        public void Unfill(MinoColor value)
        {
            IsFilled = false;
            Value = value;
        }

        public void SetValue(MinoColor value) => Value = value;
    }
}
