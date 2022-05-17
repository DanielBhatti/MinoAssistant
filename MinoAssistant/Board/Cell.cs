namespace MinoAssistant.Board
{
    public class Cell
    {
        public object? Value { get; private set; }
        public bool IsFilled { get; private set; }

        public Cell(object? value, bool isFilled)
        {
            Value = value;
            IsFilled = isFilled;
        }

        public void Fill(object? value)
        {
            IsFilled = true;
            Value = value;
        }

        public void Unfill(object? value)
        {
            IsFilled = false;
            Value = value;
        }

        public void SetValue(object? value) => Value = value;
    }
}
