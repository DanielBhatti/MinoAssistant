namespace MinoAssistant.Board
{
    public class Cell
    {
        public object? Value { get; private set; }
        public bool IsFilled { get => Value != null; }

        public void Fill(object value) => Value = value;

        public void Unfill() => Value = null;
    }
}
